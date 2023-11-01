using System;
using UnityEngine;

namespace Player
{
    [AddComponentMenu("Player.PlayerCore")]
    internal class PlayerCore : MonoBehaviour
    {
        public event Action OnDeath;

        public void GetHit()
        {
            OnDeath?.Invoke();
        }
    }
}
