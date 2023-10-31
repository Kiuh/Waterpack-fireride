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
        private new Collider2D collider;

        [SerializeField]
        private float wallRayDistance = 0.5f;

        [SerializeField]
        [InspectorReadOnly]
        private bool isTouchWall = false;
        public bool IsTouchWall => isTouchWall;

        private void Update()
        {
            CheckWall();
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

        private void CheckWall()
        {
            bool touchWall = CheckLeftWall() || CheckRightWall();

            if (touchWall && !isTouchWall)
            {
                OnWallTouch?.Invoke();
            }

            isTouchWall = touchWall;
        }

        private bool CheckLeftWall()
        {
            Vector2 leftUp = new(collider.bounds.min.x, collider.bounds.max.y);
            Vector2 leftCenter = new(collider.bounds.min.x, collider.bounds.center.y);
            Vector2 leftDown = new(collider.bounds.min.x, collider.bounds.min.y);

            bool leftUpCheck = RayCast(leftUp, Vector2.left, wallRayDistance, WALL_LAYER, WALL_TAG);
            bool leftCenterCheck = RayCast(
                leftCenter,
                Vector2.left,
                wallRayDistance,
                WALL_LAYER,
                WALL_TAG
            );
            bool leftDownCheck = RayCast(
                leftDown,
                Vector2.left,
                wallRayDistance,
                WALL_LAYER,
                WALL_TAG
            );
            return leftUpCheck || leftCenterCheck || leftDownCheck;
        }

        private bool CheckRightWall()
        {
            Vector2 rightUp = new(collider.bounds.max.x, collider.bounds.max.y);
            Vector2 rightCenter = new(collider.bounds.max.x, collider.bounds.center.y);
            Vector2 rightDown = new(collider.bounds.max.x, collider.bounds.min.y);

            bool rightUpCheck = RayCast(
                rightUp,
                Vector2.right,
                wallRayDistance,
                WALL_LAYER,
                WALL_TAG
            );
            bool rightCenterCheck = RayCast(
                rightCenter,
                Vector2.right,
                wallRayDistance,
                WALL_LAYER,
                WALL_TAG
            );
            bool rightDownCheck = RayCast(
                rightDown,
                Vector2.right,
                wallRayDistance,
                WALL_LAYER,
                WALL_TAG
            );
            return rightUpCheck || rightCenterCheck || rightDownCheck;
        }
    }
}
