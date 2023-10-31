using Common;
using UnityEngine;

namespace Player
{
    [AddComponentMenu("Scripts/Player.Fuel")]
    internal class Fuel : MonoBehaviour
    {
        [SerializeField]
        private ModifiableValueContainer fuel;
        public ModifiableValueContainer FuelValue => fuel;

        [SerializeField]
        private ModifiableValue<float> absorbingAmount;

        [SerializeField]
        [InspectorReadOnly]
        private bool absorbing;
        public bool Absorbing
        {
            get => absorbing;
            set => absorbing = value;
        }

        [SerializeField]
        [InspectorReadOnly]
        private bool canAbsorbing = true;
        public bool CanAbsorbing => canAbsorbing;

        private void FixedUpdate()
        {
            if (absorbing && canAbsorbing)
            {
                fuel.Value -= Time.fixedDeltaTime * absorbingAmount.Value;
                if (fuel.Value <= 0)
                {
                    fuel.Value = 0;
                    canAbsorbing = false;
                }
            }
        }

        public void AddFuel(float amount)
        {
            fuel.Value += amount;
            fuel.Value = Mathf.Min(fuel.Value, fuel.MaxValue);
            canAbsorbing = true;
        }

        public void FillMaxFuel()
        {
            fuel.Value = fuel.MaxValue;
            canAbsorbing = true;
        }
    }
}
