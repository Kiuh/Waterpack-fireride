using Common;
using Player;
using UnityEngine;

namespace Jetpack
{
    [AddComponentMenu("Jetpack.PickUp")]
    internal class PickUp : MonoBehaviour
    {
        public const string PLAYER_TAG = "Player";

        [SerializeField]
        private ModifiableValueContainer fillAmount;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(PLAYER_TAG) && collision.TryGetComponent(out Fuel fuel))
            {
                fuel.AddFuel(fillAmount.Value);
                Destroy(gameObject);
            }
        }
    }
}
