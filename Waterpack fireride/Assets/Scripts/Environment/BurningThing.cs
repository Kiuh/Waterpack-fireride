using Jetpack;
using Player;
using System;
using UnityEngine;

namespace Environment
{
    [Serializable]
    public struct BurningInfo
    {
        public int FireAmount;
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
        public event Action<int> OnFireAdding;
        public event Action<ContactPoint2D[], int> OnWaterAbsorbing;

        public void SetBurningInfo(BurningInfo burningInfo)
        {
            this.burningInfo = burningInfo;
            OnFireAdding?.Invoke(this.burningInfo.FireAmount);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!isActive)
            {
                return;
            }
            if (collision.collider.CompareTag(PLYER_TAG))
            {
                CollideWithPlayer(collision);
            }
            else if (collision.collider.CompareTag(WATER_TAG))
            {
                CollideWithWater(collision);
            }
        }

        private void CollideWithPlayer(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out PlayerCore playerCore))
            {
                playerCore.GetHit();
            }
        }

        private void CollideWithWater(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out WaterPiece waterPiece))
            {
                OnWaterAbsorbing?.Invoke(collision.contacts, waterPiece.WaterInfo.Extinguishing);
                AbsorbWater(waterPiece);
            }
        }

        private void AbsorbWater(WaterPiece waterPiece)
        {
            burningInfo.FireAmount -= waterPiece.WaterInfo.Extinguishing;

            if (burningInfo.FireAmount <= 0)
            {
                isActive = false;
                OnInActive?.Invoke();
            }
            Destroy(waterPiece.gameObject);
        }
    }
}
