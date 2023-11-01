using Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Environment
{
    public struct PositionedParticle
    {
        public Vector2 Position;
        public GameObject FireObject;
    }

    [AddComponentMenu("Environment.BurningView")]
    internal class BurningView : MonoBehaviour
    {
        [SerializeField]
        private PolygonCollider2D polygonCollider;

        [SerializeField]
        private GameObject fireParticlePrefab;

        [SerializeField]
        private BurningThing burningThing;

        [SerializeField]
        private uint firersPerAmount;

        [SerializeField]
        [InspectorReadOnly]
        private uint firersShowing;

        private List<PositionedParticle> positionedParticles = new();

        private void Awake()
        {
            burningThing.OnFireAdding += BurningThing_OnFireAdding;
            burningThing.OnWaterAbsorbing += BurningThing_OnWaterAbsorbing;
            burningThing.OnInActive += BurningThing_OnInActive;
        }

        private void Start()
        {
            if (burningThing.IsUnique)
            {
                BurningThing_OnFireAdding();
            }
        }

        private void BurningThing_OnInActive()
        {
            while (positionedParticles.Count > 0)
            {
                Destroy(positionedParticles.Last().FireObject);
                _ = positionedParticles.Remove(positionedParticles.Last());
            }
        }

        private void BurningThing_OnFireAdding()
        {
            uint delta = burningThing.BurningInfo.FireAmount - firersShowing;
            if (delta > 0)
            {
                for (int i = 0; i < delta / firersPerAmount; i++)
                {
                    positionedParticles.Add(SpawnFire());
                }
                firersShowing = burningThing.BurningInfo.FireAmount;
            }
        }

        private PositionedParticle SpawnFire()
        {
            Vector2 position = PointInArea(polygonCollider);
            GameObject fire = Instantiate(
                fireParticlePrefab,
                position,
                fireParticlePrefab.transform.rotation,
                transform
            );
            return new PositionedParticle() { FireObject = fire, Position = position };
        }

        private void BurningThing_OnWaterAbsorbing(ContactPoint2D contact)
        {
            uint delta = burningThing.BurningInfo.FireAmount - firersShowing;
            if (delta < 0)
            {
                positionedParticles = positionedParticles
                    .OrderBy(x => Vector2.Distance(x.Position, contact.point))
                    .ToList();
                for (int i = 0; i < (-delta) / firersPerAmount; i++)
                {
                    if (positionedParticles.Count > 0)
                    {
                        Destroy(positionedParticles.First().FireObject);
                        _ = positionedParticles.Remove(positionedParticles.First());
                    }
                }
            }
        }

        public const uint ATTEMPTS = 150;

        public Vector2 PointInArea(Collider2D collider)
        {
            Vector2 point;
            Bounds bounds = collider.bounds;
            int attempt = 0;

            float x;
            float y;
            do
            {
                x = Random.Range(bounds.max.x, bounds.min.x);
                y = Random.Range(bounds.max.y, bounds.min.y);
                attempt++;
                point = new Vector2(x, y);
            } while (!collider.OverlapPoint(point) && attempt <= ATTEMPTS);
            return point;
        }
    }
}
