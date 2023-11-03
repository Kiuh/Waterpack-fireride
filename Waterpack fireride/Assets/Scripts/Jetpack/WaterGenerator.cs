using Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jetpack
{
    [Serializable]
    public struct GenerationInfo
    {
        public float StartForceUp;
        public float StartForceDown;
        public float AngleDelta;
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
        private float waterGenerationPerFrame;

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

        private void FixedUpdate()
        {
            if (generating)
            {
                for (int i = 0; i < waterGenerationPerFrame; ++i)
                {
                    ProduceWater();
                }
            }
        }

        private void ProduceWater()
        {
            Vector3 point = produceDirectionPoints
                .Find(x => x.ProduceDirection == produceDirection.ToOpposite())
                .Transform.position;
            WaterPiece water = Instantiate(
                waterPiecePrefab,
                point,
                waterPiecePrefab.transform.rotation
            );
            water.RigidBody2D.AddForce(
                Quaternion.Euler(
                    Vector3.forward
                        * UnityEngine.Random.Range(
                            -generationInfo.AngleDelta,
                            generationInfo.AngleDelta
                        )
                )
                    * Vector2.up
                    * produceDirection.ToOpposite().ToFloat()
                    * (
                        produceDirection.ToOpposite() == VerticalDirection.Up
                            ? generationInfo.StartForceUp
                            : generationInfo.StartForceDown
                    ),
                ForceMode2D.Impulse
            );
            water.SetInfo(waterInfo);
        }
    }
}
