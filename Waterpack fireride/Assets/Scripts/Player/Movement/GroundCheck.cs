using Common;
using System;
using UnityEngine;

namespace Player.Movement
{
    [AddComponentMenu("Scripts/Player.Movement.GroundCheck")]
    public class GroundCheck : MonoBehaviour
    {
        public event Action OnGroundTouch;

        public const string GROUND_TAG = "Ground";
        public const string GROUND_LAYER = "Grounds";

        [SerializeField]
        private new Collider2D collider;

        [SerializeField]
        private float groundRayDistance = 0.5f;

        [SerializeField]
        [InspectorReadOnly]
        private bool isTouchGround = false;
        public bool IsTouchGround => isTouchGround;

        private void Awake()
        {
            isTouchGround = false;
        }

        private void Update()
        {
            CheckGround();
        }

        private bool RayCast(
            Vector2 origin,
            Vector2 direction,
            float rayDistance,
            string tagLayer,
            string tag
        )
        {
            RaycastHit2D hit1 = Physics2D.Raycast(
                origin,
                direction,
                rayDistance,
                LayerMask.GetMask(tagLayer)
            );
            Debug.DrawRay(origin, direction * rayDistance, Color.red);
            return hit1.collider != null && hit1.collider.CompareTag(tag);
        }

        private void CheckGround()
        {
            Vector2 left = new(collider.bounds.max.x, collider.bounds.center.y);
            Vector2 center = new(collider.bounds.center.x, collider.bounds.center.y);
            Vector2 right = new(collider.bounds.min.x, collider.bounds.center.y);

            bool grounded1 = RayCast(
                left,
                Vector2.down,
                groundRayDistance,
                GROUND_LAYER,
                GROUND_TAG
            );

            bool grounded2 = RayCast(
                center,
                Vector2.down,
                groundRayDistance,
                GROUND_LAYER,
                GROUND_TAG
            );

            bool grounded3 = RayCast(
                right,
                Vector2.down,
                groundRayDistance,
                GROUND_LAYER,
                GROUND_TAG
            );

            bool grounded = grounded1 || grounded2 || grounded3;

            if (grounded && !isTouchGround)
            {
                OnGroundTouch?.Invoke();
            }

            isTouchGround = grounded;
        }
    }
}
