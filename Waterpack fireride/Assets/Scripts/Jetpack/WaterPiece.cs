using System;
using UnityEngine;

namespace Jetpack
{
    [Serializable]
    public struct WaterInfo
    {
        public float Extinguishing;
        public float LifeTime;
    }

    [AddComponentMenu("Jetpack.WaterPiece")]
    internal class WaterPiece : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rigidBody2D;
        public Rigidbody2D RigidBody2D => rigidBody2D;

        private WaterInfo waterInfo;
        public WaterInfo WaterInfo => waterInfo;

        public void SetInfo(WaterInfo waterInfo)
        {
            this.waterInfo = waterInfo;
            Destroy(gameObject, waterInfo.LifeTime);
        }
    }
}
