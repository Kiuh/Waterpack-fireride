using Jetpack;
using UnityEngine;

namespace Player
{
    [AddComponentMenu("Scripts/Player.Controller")]
    internal class Controller : MonoBehaviour
    {
        [SerializeField]
        private Movement movement;

        [SerializeField]
        private CollisionCheck collisionCheck;

        [SerializeField]
        private Fuel fuel;

        [SerializeField]
        private Stamina stamina;

        [SerializeField]
        private View view;

        [SerializeField]
        private WaterGenerator waterGenerator;

        private void Awake()
        {
            collisionCheck.OnGroundTouch += stamina.FillMaxStamina;
            collisionCheck.OnWallTouch += view.Flip;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                fuel.Absorbing = true;
                stamina.Absorbing = true;
            }
            else
            {
                fuel.Absorbing = false;
                stamina.Absorbing = false;
            }

            if (fuel.Absorbing && fuel.CanAbsorbing && stamina.CanAbsorbing)
            {
                movement.Flying = true;
                waterGenerator.Generating = true;
            }
            else
            {
                movement.Flying = false;
                waterGenerator.Generating = false;
            }
        }
    }
}
