using Common;
using UnityEngine;

namespace Player
{
    [AddComponentMenu("Scripts/Player.Movement")]
    internal class Movement : MonoBehaviour
    {
        [Header("Movement Details")]
        [Space]
        [SerializeField]
        private float runSpeed;

        [SerializeField]
        private float flyStrength;

        [Header("Movement Reference")]
        [Space]
        [SerializeField]
        private Rigidbody2D rigidBody2D;

        [SerializeField]
        private CollisionCheck collisionCheck;

        [Header("Readonly Values")]
        [Space]
        [SerializeField]
        [InspectorReadOnly]
        private bool flying;
        public bool Flying
        {
            get => flying;
            set => flying = value;
        }

        [SerializeField]
        [InspectorReadOnly]
        private RunDirection runDirection = RunDirection.Right;

        private void Awake()
        {
            collisionCheck.OnWallTouch += CollisionCheck_OnWallTouch;
        }

        private void CollisionCheck_OnWallTouch()
        {
            runDirection = runDirection.ToOpposite();
        }

        private void FixedUpdate()
        {
            Move(runDirection);
            if (flying)
            {
                Fly();
            }
        }

        private void Move(RunDirection direction)
        {
            Vector2 velocity = rigidBody2D.velocity;
            velocity.x = runSpeed * direction.ToFloat();
            rigidBody2D.velocity = velocity;
        }

        private void Fly()
        {
            Vector2 velocity = rigidBody2D.velocity;
            velocity.y = flyStrength;
            rigidBody2D.velocity = velocity;
        }
    }
}
