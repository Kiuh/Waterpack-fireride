using Common;
using UnityEngine;

namespace Player.Movement
{
    [AddComponentMenu("Player.Movement.Running")]
    internal class Running : MonoBehaviour
    {
        [SerializeField]
        private float runSpeed;

        [SerializeField]
        private Rigidbody2D rigidBody2D;

        [SerializeField]
        private WallCheck wallCheck;

        [SerializeField]
        [InspectorReadOnly]
        private HorizontalDirection runDirection = HorizontalDirection.Right;

        private void Awake()
        {
            wallCheck.OnWallTouch += CollisionCheck_OnWallTouch;
        }

        private void CollisionCheck_OnWallTouch()
        {
            runDirection = runDirection.ToOpposite();
        }

        private void FixedUpdate()
        {
            Move(runDirection);
        }

        private void Move(HorizontalDirection direction)
        {
            Vector2 velocity = rigidBody2D.velocity;
            velocity.x = runSpeed * direction.ToFloat();
            rigidBody2D.velocity = velocity;
        }
    }
}
