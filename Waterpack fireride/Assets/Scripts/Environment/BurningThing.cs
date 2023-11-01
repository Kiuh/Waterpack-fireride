using Jetpack;
using Player;
using System;
using System.Linq;
using UnityEngine;

namespace Environment
{
    [Serializable]
    public struct BurningInfo
    {
        public uint FireAmount;
    }

    [AddComponentMenu("Environment.BurningThing")]
    internal class BurningThing : MonoBehaviour
    {
        public const string PLYER_TAG = "Player";
        public const string WATER_TAG = "Water";

        [SerializeField]
        private bool isUnique = false;
        public bool IsUnique => isUnique;

        [SerializeField]
        private BurningInfo burningInfo;
        public BurningInfo BurningInfo => burningInfo;

        [SerializeField]
        private bool isActive = true;
        public bool IsActive => isActive;

        public event Action OnInActive;
        public event Action OnFireAdding;
        public event Action<ContactPoint2D> OnWaterAbsorbing;

        public void SetBurningInfo(BurningInfo burningInfo)
        {
            this.burningInfo = burningInfo;
            OnFireAdding?.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!isActive)
            {
                return;
            }
            Debug.Log(
                collision.contacts
                    .Select(x => x.otherCollider.tag)
                    .Aggregate("", (x, y) => x + " " + y)
            );
            if (collision.contacts.Any(x => x.otherCollider.CompareTag(PLYER_TAG)))
            {
                foreach (
                    ContactPoint2D item in collision.contacts.Where(
                        x => x.otherCollider.CompareTag(PLYER_TAG)
                    )
                )
                {
                    CollideWithPlayer(item);
                }
            }
            else if (collision.contacts.Any(x => x.otherCollider.CompareTag(WATER_TAG)))
            {
                foreach (
                    ContactPoint2D item in collision.contacts.Where(
                        x => x.otherCollider.CompareTag(WATER_TAG)
                    )
                )
                {
                    CollideWithWater(item);
                }
            }
        }

        private void CollideWithPlayer(ContactPoint2D collision)
        {
            if (collision.otherCollider.TryGetComponent(out PlayerCore playerCore))
            {
                playerCore.GetHit();
            }
        }

        private void CollideWithWater(ContactPoint2D collision)
        {
            if (collision.otherCollider.TryGetComponent(out WaterPiece waterPiece))
            {
                AbsorbWater(waterPiece);
                OnWaterAbsorbing?.Invoke(collision);
            }
        }

        private void AbsorbWater(WaterPiece waterPiece)
        {
            burningInfo.FireAmount -= waterPiece.WaterInfo.Extinguishing;

            if (burningInfo.FireAmount < 0)
            {
                isActive = false;
                OnInActive?.Invoke();
            }
            Destroy(waterPiece.gameObject);
        }
    }
}
