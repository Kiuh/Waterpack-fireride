using Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    [AddComponentMenu("Player.DragHandler")]
    internal class DragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField]
        private float swipeThreshold = 100f;

        [SerializeField]
        [InspectorReadOnly]
        private Direction direction;
        public Direction Direction => direction;

        [SerializeField]
        [InspectorReadOnly]
        private bool draggingStarted;
        public bool DraggingStarted => draggingStarted;

        [SerializeField]
        [InspectorReadOnly]
        private Vector2 startPosition;

        [SerializeField]
        [InspectorReadOnly]
        private Vector2 endPosition;

        private void Awake()
        {
            draggingStarted = false;
            direction = Direction.None;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            draggingStarted = true;
            startPosition = eventData.pressPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (draggingStarted)
            {
                endPosition = eventData.position;

                Vector2 difference = endPosition - startPosition;

                if (difference.magnitude > swipeThreshold)
                {
                    if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
                    {
                        direction = difference.x > 0 ? Direction.Right : Direction.Left;
                    }
                    else
                    {
                        direction = difference.y > 0 ? Direction.Up : Direction.Down;
                    }
                }
                else
                {
                    direction = Direction.None;
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            draggingStarted = false;
        }
    }
}
