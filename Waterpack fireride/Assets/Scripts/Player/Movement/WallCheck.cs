using Common;
using System;
using UnityEngine;

namespace Player.Movement
{
    [AddComponentMenu("Player.Movement.WallCheck")]
    internal class WallCheck : MonoBehaviour
    {
        public event Action OnWallTouch;

        public const string WALL_TAG = "Wall";
        public const string WALL_LAYER = "Walls";

        [SerializeField]
        private float touchDelay;

        [SerializeField]
        [InspectorReadOnly]
        private float timer;

        private void Update()
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag(WALL_TAG) && timer <= 0)
            {
                OnWallTouch?.Invoke();
                timer = touchDelay;
            }
        }
    }
}
