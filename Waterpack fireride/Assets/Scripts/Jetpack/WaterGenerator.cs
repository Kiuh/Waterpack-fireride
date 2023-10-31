using Common;
using System;
using UnityEngine;

namespace Jetpack
{
    [Serializable]
    public struct GenerationInfo
    {
        public float StartForce;
        public float DegreesDelta;
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
        private Transform generationPoint;

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
            WaterPiece water = Instantiate(
                waterPiecePrefab,
                generationPoint.transform.position,
                waterPiecePrefab.transform.rotation
            );
            water.RigidBody2D.AddForce(
                Quaternion.Euler(
                    Vector3.up
                        * UnityEngine.Random.Range(
                            -generationInfo.DegreesDelta,
                            generationInfo.DegreesDelta
                        )
                )
                    * Vector2.down
                    * generationInfo.StartForce,
                ForceMode2D.Impulse
            );
            water.SetInfo(waterInfo);
        }
    }
}
