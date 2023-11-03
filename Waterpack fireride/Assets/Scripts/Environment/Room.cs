using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Environment
{
    public enum PieceType
    {
        Wall,
        Corner
    }

    [Serializable]
    public struct Ignoring
    {
        public Direction Direction;
        public PieceType PieceType;
        public int Index;
    }

    [AddComponentMenu("Environment.Room")]
    public class Room : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int size;

        [SerializeField]
        private float yPieceSize;

        [SerializeField]
        private GameObject yPiece;

        [SerializeField]
        private float xPieceSize;

        [SerializeField]
        private GameObject xPiece;

        [SerializeField]
        private GameObject corner;

        [SerializeField]
        private List<Ignoring> ignoreList;

        [SerializeField]
        private GameObject backTilePrefab;

        [SerializeField]
        private Vector2Int backgroundSize;

        [SerializeField]
        private Vector2 backgroundSpaces;

        public void Recreate()
        {
# if UNITY_EDITOR

            while (transform.childCount > 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
            CreateCornerCorners();
            CreateCorners();
            CreateWalls();
            FillBackground();
#endif
        }

#if UNITY_EDITOR

        private void FillBackground()
        {
            for (int i = 1; i <= backgroundSize.x; i++)
            {
                for (int j = 1; j <= backgroundSize.y; j++)
                {
                    GameObject spawn =
                        PrefabUtility.InstantiatePrefab(backTilePrefab, transform) as GameObject;
                    spawn.transform.localPosition = new Vector3(
                        (backgroundSpaces.x * i) - (xPieceSize / 2),
                        (backgroundSpaces.y * j) - (yPieceSize / 2)
                    );
                }
            }
        }

        private void CreateCornerCorners()
        {
            GameObject spawn;
            spawn = PrefabUtility.InstantiatePrefab(corner, transform) as GameObject;
            spawn.transform.localPosition = new Vector3(-(xPieceSize / 2), -(yPieceSize / 2));
            spawn = PrefabUtility.InstantiatePrefab(corner, transform) as GameObject;
            spawn.transform.localPosition = new Vector3(
                -(xPieceSize / 2),
                (xPieceSize * size.y) - (yPieceSize / 2)
            );
            spawn = PrefabUtility.InstantiatePrefab(corner, transform) as GameObject;
            spawn.transform.localPosition = new Vector3(
                (xPieceSize * size.x) - (xPieceSize / 2),
                -(yPieceSize / 2)
            );
            spawn = PrefabUtility.InstantiatePrefab(corner, transform) as GameObject;
            spawn.transform.localPosition = new Vector3(
                (xPieceSize * size.x) - (xPieceSize / 2),
                (xPieceSize * size.y) - (yPieceSize / 2)
            );
        }

        private void CreateCorners()
        {
            IEnumerable<Ignoring> iterationList = ignoreList.Where(
                x => x.PieceType == PieceType.Corner
            );
            for (int i = 0; i < size.x - 1; i++)
            {
                if (
                    iterationList
                        .Where(x => x.Direction == Direction.Up)
                        .Select(x => x.Index)
                        .Contains(i)
                )
                {
                    continue;
                }

                GameObject spawn = PrefabUtility.InstantiatePrefab(corner, transform) as GameObject;
                spawn.transform.localPosition = new Vector3(
                    (xPieceSize * i) + (xPieceSize / 2),
                    (yPieceSize * size.y) - (yPieceSize / 2)
                );
            }
            for (int i = 0; i < size.x - 1; i++)
            {
                if (
                    iterationList
                        .Where(x => x.Direction == Direction.Down)
                        .Select(x => x.Index)
                        .Contains(i)
                )
                {
                    continue;
                }

                GameObject spawn = PrefabUtility.InstantiatePrefab(corner, transform) as GameObject;
                spawn.transform.localPosition = new Vector3(
                    (xPieceSize * i) + (xPieceSize / 2),
                    -(yPieceSize / 2)
                );
            }
            for (int i = 0; i < size.y - 1; i++)
            {
                if (
                    iterationList
                        .Where(x => x.Direction == Direction.Right)
                        .Select(x => x.Index)
                        .Contains(i)
                )
                {
                    continue;
                }

                GameObject spawn = PrefabUtility.InstantiatePrefab(corner, transform) as GameObject;
                spawn.transform.localPosition = new Vector3(
                    (xPieceSize * size.x) - (xPieceSize / 2),
                    (yPieceSize * i) + (yPieceSize / 2)
                );
            }
            for (int i = 0; i < size.y - 1; i++)
            {
                if (
                    iterationList
                        .Where(x => x.Direction == Direction.Left)
                        .Select(x => x.Index)
                        .Contains(i)
                )
                {
                    continue;
                }

                GameObject spawn = PrefabUtility.InstantiatePrefab(corner, transform) as GameObject;
                spawn.transform.localPosition = new Vector3(
                    -(xPieceSize / 2),
                    (yPieceSize * i) + (yPieceSize / 2)
                );
            }
        }

        private void CreateWalls()
        {
            IEnumerable<Ignoring> iterationList = ignoreList.Where(
                x => x.PieceType == PieceType.Wall
            );
            for (int i = 0; i < size.x; i++)
            {
                if (
                    iterationList
                        .Where(x => x.Direction == Direction.Up)
                        .Select(x => x.Index)
                        .Contains(i)
                )
                {
                    continue;
                }
                GameObject spawn = PrefabUtility.InstantiatePrefab(xPiece, transform) as GameObject;
                spawn.transform.localPosition = new Vector3(
                    xPieceSize * i,
                    (yPieceSize * size.y) - (yPieceSize / 2),
                    0
                );
            }
            for (int i = 0; i < size.x; i++)
            {
                if (
                    iterationList
                        .Where(x => x.Direction == Direction.Down)
                        .Select(x => x.Index)
                        .Contains(i)
                )
                {
                    continue;
                }
                GameObject spawn = PrefabUtility.InstantiatePrefab(xPiece, transform) as GameObject;
                spawn.transform.localPosition = new Vector3(xPieceSize * i, -(yPieceSize / 2), 0);
            }
            for (int i = 0; i < size.y; i++)
            {
                if (
                    iterationList
                        .Where(x => x.Direction == Direction.Right)
                        .Select(x => x.Index)
                        .Contains(i)
                )
                {
                    continue;
                }
                GameObject spawn = PrefabUtility.InstantiatePrefab(yPiece, transform) as GameObject;
                spawn.transform.localPosition = new Vector3(
                    (xPieceSize * size.x) - (xPieceSize / 2),
                    yPieceSize * i,
                    0
                );
            }
            for (int i = 0; i < size.y; i++)
            {
                if (
                    iterationList
                        .Where(x => x.Direction == Direction.Left)
                        .Select(x => x.Index)
                        .Contains(i)
                )
                {
                    continue;
                }
                GameObject spawn = PrefabUtility.InstantiatePrefab(yPiece, transform) as GameObject;
                spawn.transform.localPosition = new Vector3(-(xPieceSize / 2), yPieceSize * i, 0);
            }
        }
#endif
    }
}
