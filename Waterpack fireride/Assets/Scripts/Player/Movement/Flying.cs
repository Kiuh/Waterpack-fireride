using Common;
using UnityEngine;

namespace Player.Movement
{
    [AddComponentMenu("Scripts/Player.Movement.Flying")]
    internal class Flying : MonoBehaviour
    {
        [SerializeField]
        private float flyStrength;

        [SerializeField]
        private Rigidbody2D rigidBody2D;

        [SerializeField]
        private GroundCheck collisionCheck;

        [SerializeField]
        [InspectorReadOnly]
        private bool isFlying;
        public bool IsFlying
        {
            get => isFlying;
            set => isFlying = value;
        }

        [SerializeField]
        [InspectorReadOnly]
        private VerticalDirection flyDirection = VerticalDirection.Up;
        public VerticalDirection FlyDirection
        {
            get => flyDirection;
            set => flyDirection = value;
        }

        private void FixedUpdate()
        {
            if (isFlying)
            {
                Fly();
            }
        }

        private void Fly()
        {
            Vector2 velocity = rigidBody2D.velocity;
            velocity.y = flyStrength * flyDirection.ToFloat();
            rigidBody2D.velocity = velocity;
        }
    }
}
