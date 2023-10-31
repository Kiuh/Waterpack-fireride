using Common;
using UnityEngine;

namespace Player.Movement
{
    [AddComponentMenu("Player.Movement.Jumping")]
    internal class Jumping : MonoBehaviour
    {
        [SerializeField]
        private GroundCheck groundCheck;

        [SerializeField]
        private Rigidbody2D rigidBody2D;

        [SerializeField]
        private float jumpStrength;

        [SerializeField]
        private float jumpCoolDown;

        [SerializeField]
        [InspectorReadOnly]
        private float timer = 0;

        private void Update()
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
        }

        public void Jump()
        {
            if (timer >= 0 || !groundCheck.IsTouchGround)
            {
                return;
            }

            rigidBody2D.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            timer = jumpCoolDown;
        }
    }
}
