﻿using System;
using UnityEngine;

namespace Common
{
    [Serializable]
    public class ModifiableValueContainer
    {
        [SerializeField]
        private ModifiableValue<float> maxValue = new(0);
        public float BaseValue
        {
            get => maxValue.SourceValue;
            set => maxValue.SourceValue = value;
        }

        public float MaxValue => maxValue.Value;

        [SerializeField]
        private float currentValue;
        public float Value
        {
            get => currentValue;
            set => currentValue = value;
        }

        [SerializeField]
        private float minValue;

        public ModifiableValueContainer(float minValue, float maxSourceValue, float currentValue)
        {
            this.minValue = minValue;
            maxValue = new(maxSourceValue);
            this.currentValue = currentValue;
        }

        public void AddModification(ValueModification<float> modification)
        {
            float oldMaxValue = maxValue.Value;
            maxValue.Modifications.Add(modification);
            float newMaxValue = maxValue.Value;
            currentValue = TransformToNewValue(oldMaxValue, newMaxValue, currentValue, minValue);
        }

        public void RemoveModification(ValueModification<float> modification)
        {
            float oldMaxValue = maxValue.Value;
            _ = maxValue.Modifications.Remove(modification);
            float newMaxValue = maxValue.Value;
            currentValue = TransformToNewValue(oldMaxValue, newMaxValue, currentValue, minValue);
        }

        private float TransformToNewValue(
            float oldMaxValue,
            float newMaxValue,
            float oldValue,
            float minValue
        )
        {
            return ((newMaxValue - minValue) * (oldValue - minValue) / (oldMaxValue - minValue))
                + minValue;
        }
    }
}
