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
        private uint amountsPerFire;

        private List<PositionedParticle> positionedParticles = new();
        private PositionedParticle positionedParticle;

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
                BurningThing_OnFireAdding(burningThing.BurningInfo.FireAmount);
            }
        }

        private void BurningThing_OnInActive()
        {
            while (positionedParticles.Count > 0)
            {
                Destroy(positionedParticles.Last().FireObject);
                _ = positionedParticles.Remove(positionedParticles.Last());
            }
            Destroy(positionedParticle.FireObject);
        }

        private void BurningThing_OnFireAdding(int amount)
        {
            positionedParticle = SpawnFire();
            for (int i = 0; i < amount / amountsPerFire; i++)
            {
                positionedParticles.Add(SpawnFire());
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

        private void BurningThing_OnWaterAbsorbing(ContactPoint2D[] contacts, int amount)
        {
            Vector2 point =
                (
                    contacts.Select(x => x.point).Aggregate(Vector2.zero, (x, y) => x + y)
                    / contacts.Length
                ) + (Vector2)transform.position;
            positionedParticles = positionedParticles
                .OrderBy(x => Vector2.Distance(x.Position, point))
                .ToList();

            while (burningThing.BurningInfo.FireAmount < positionedParticles.Count * amountsPerFire)
            {
                Destroy(positionedParticles.Last().FireObject);
                _ = positionedParticles.Remove(positionedParticles.Last());
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
