using Common;
using Jetpack;
using Player.Movement;
using System.Linq;
using UnityEngine;

namespace Player
{
    [AddComponentMenu("Scripts/Player.Controller")]
    internal class Controller : MonoBehaviour
    {
        [SerializeField]
        private Flying flyMovement;

        [SerializeField]
        private Jumping jumping;

        [SerializeField]
        private GroundCheck groundCheck;

        [SerializeField]
        private WallCheck wallCheck;

        [SerializeField]
        private Fuel fuel;

        [SerializeField]
        private Stamina stamina;

        [SerializeField]
        private View view;

        [SerializeField]
        private WaterGenerator waterGenerator;

        [SerializeField]
        private DragHandler dragHandler;

        [SerializeField]
        private float neededTouchDelta;

        [SerializeField]
        [InspectorReadOnly]
        private float touchDelta;

        private void Awake()
        {
            groundCheck.OnGroundTouch += stamina.FillMaxStamina;
            //wallCheck.OnWallTouch += view.Flip;
        }

        private void FixedUpdate()
        {
            if (groundCheck.IsTouchGround)
            {
                stamina.FillMaxStamina();
            }
        }

        private void Update()
        {
            if (
                Input.touchCount > 0
                && RayCastUtilities
                    .UIRayCast(Input.GetTouch(0).position)
                    .Where(x => x.CompareTag("OverlayUI"))
                    .Count() > 0
            )
            {
                return;
            }

            if (Input.touchCount > 0)
            {
                touchDelta += Time.deltaTime;
            }
            else
            {
                touchDelta = 0;
            }

            if (
                Input.touchCount > 0
                && Input.GetTouch(0).phase == TouchPhase.Ended
                && touchDelta <= neededTouchDelta
            )
            {
                //   jumping.Jump();
            }
            else if (
                Input.touchCount > 0
                && dragHandler.DraggingStarted
                && dragHandler.Direction != Direction.None
                && dragHandler.Direction != Direction.Left
                && dragHandler.Direction != Direction.Right
            )
            {
                fuel.Absorbing = true;
                stamina.Absorbing = true;
                flyMovement.FlyDirection =
                    dragHandler.Direction == Direction.Up
                        ? VerticalDirection.Up
                        : VerticalDirection.Down;
                waterGenerator.ProduceDirection = flyMovement.FlyDirection;
            }
            else
            {
                fuel.Absorbing = false;
                stamina.Absorbing = false;
            }

            if (fuel.Absorbing && fuel.CanAbsorbing && stamina.CanAbsorbing)
            {
                flyMovement.IsFlying = true;
                waterGenerator.Generating = true;
            }
            else
            {
                flyMovement.IsFlying = false;
                waterGenerator.Generating = false;
            }
        }
    }
}
