using Player.Movement;
using UnityEngine;

namespace Player
{
    [AddComponentMenu("Player.View")]
    internal class View : MonoBehaviour
    {
        [SerializeField]
        private Running running;

        private void Update()
        {
            transform.rotation = Quaternion.Euler(
                running.HorizontalDirection == Common.HorizontalDirection.Left
                    ? Vector3.up * 180
                    : Vector3.zero
            );
        }
    }
}
