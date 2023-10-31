using Common;
using UnityEngine;

namespace Player
{
    [AddComponentMenu("Player.Stamina")]
    internal class Stamina : MonoBehaviour
    {
        [SerializeField]
        private ModifiableValueContainer stamina;
        public ModifiableValueContainer StaminaValue => stamina;

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
                stamina.Value -= Time.fixedDeltaTime * absorbingAmount.Value;
                if (stamina.Value <= 0)
                {
                    stamina.Value = 0;
                    canAbsorbing = false;
                }
            }
        }

        public void AddStamina(float amount)
        {
            stamina.Value += amount;
            stamina.Value = Mathf.Min(stamina.Value, stamina.MaxValue);
            canAbsorbing = true;
        }

        public void FillMaxStamina()
        {
            stamina.Value = stamina.MaxValue;
            canAbsorbing = true;
        }
    }
}
