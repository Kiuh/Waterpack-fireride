using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Environment
{
    [AddComponentMenu("Environment.BurningManager")]
    internal class BurningManager : MonoBehaviour
    {
        [SerializeField]
        private BurningInfo defaultBurningInfo;
        private IEnumerable<BurningThing> burningThings;

        public event Action OnAllInactive;

        private void Start()
        {
            burningThings = FindObjectsByType<BurningThing>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );
            foreach (BurningThing item in burningThings)
            {
                item.OnInActive += OnSomeoneInactive;
            }
            foreach (BurningThing item in burningThings.Where(x => !x.IsUnique))
            {
                item.SetBurningInfo(defaultBurningInfo);
            }
        }

        private void OnSomeoneInactive()
        {
            if (burningThings.All(x => !x.IsActive))
            {
                OnAllInactive?.Invoke();
            }
        }
    }
}
