﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    public enum ModificationPriority
    {
        PreAddiction,
        PreMultiplication,
        Addiction,
        Multiplication,
        PostAddiction,
        PostMultiplication,
    }

    public class ValueModification<T>
    {
        public Func<T, T> Func { get; set; }
        public ModificationPriority Priority;

        public ValueModification(Func<T, T> func, ModificationPriority priority)
        {
            Func = func;
            Priority = priority;
        }
    }

    [Serializable]
    public class ModifiableValue<T>
    {
        [SerializeField]
        private T sourceValue;

        private List<ValueModification<T>> modifications = new();
        public List<ValueModification<T>> Modifications
        {
            get
            {
                modifications ??= new List<ValueModification<T>>();
                return modifications;
            }
        }
        public T SourceValue
        {
            get => sourceValue;
            set => sourceValue = value;
        }

        public ModifiableValue(T sourceValue)
        {
            SourceValue = sourceValue;
        }

        public T Value
        {
            get
            {
                T value = SourceValue;
                foreach (
                    ValueModification<T> modification in Modifications.OrderBy(x => (int)x.Priority)
                )
                {
                    value = modification.Func(value);
                }
                return value;
            }
        }
    }
}
