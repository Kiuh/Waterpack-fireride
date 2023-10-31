using Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jetpack
{
    [Serializable]
    public struct GenerationInfo
    {
        public float StartForce;
    }

    [Serializable]
    public struct TransformWithDirection
    {
        public Transform Transform;
        public VerticalDirection ProduceDirection;
    }

    [AddComponentMenu("Jetpack.WaterGenerator")]
    internal class WaterGenerator : MonoBehaviour
    {
        [SerializeField]
        private WaterInfo waterInfo;

        [SerializeField]
        private GenerationInfo generationInfo;

        [SerializeField]
        private WaterPiece waterPiecePrefab;

        [SerializeField]
        private float waterGenerationPerSecond;

        [SerializeField]
        private List<TransformWithDirection> produceDirectionPoints;

        [SerializeField]
        [InspectorReadOnly]
        private bool generating;

        public bool Generating
        {
            get => generating;
            set => generating = value;
        }

        [SerializeField]
        [InspectorReadOnly]
        private VerticalDirection produceDirection;

        public VerticalDirection ProduceDirection
        {
            get => produceDirection;
            set => produceDirection = value;
        }

        [SerializeField]
        [InspectorReadOnly]
        private float timer;

        private float GenerationCoolDown => 1f / waterGenerationPerSecond;

        private void Awake()
        {
            timer = GenerationCoolDown;
        }

        private void FixedUpdate()
        {
            if (generating)
            {
                timer -= Time.fixedDeltaTime;
                if (timer < 0f)
                {
                    ProduceWater();
                    if (Math.Abs(timer) > GenerationCoolDown)
                    {
                        for (int i = 0; i < (int)(timer / GenerationCoolDown); i++)
                        {
                            ProduceWater();
                        }
                        timer = GenerationCoolDown + (timer % GenerationCoolDown);
                    }
                    else
                    {
                        timer = GenerationCoolDown;
                    }
                }
            }
        }

        private void ProduceWater()
        {
            Vector3 point = produceDirectionPoints
                .Find(x => x.ProduceDirection == produceDirection)
                .Transform.position;
            WaterPiece water = Instantiate(
                waterPiecePrefab,
                point,
                waterPiecePrefab.transform.rotation
            );
            water.RigidBody2D.AddForce(
                Quaternion.Euler(Vector3.up * produceDirection.ToFloat())
                    * Vector2.down
                    * generationInfo.StartForce,
                ForceMode2D.Impulse
            );
            water.SetInfo(waterInfo);
        }
    }
}
