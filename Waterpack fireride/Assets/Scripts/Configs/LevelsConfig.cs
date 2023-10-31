using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/LevelsConfig", order = 1)]
    internal class LevelsConfig : ScriptableObject
    {
        [SerializeField]
        private List<Level> levels;
        public List<Level> Levels => levels;

        public Level GetCurrentLevel()
        {
            return Levels.Where(level => level.LevelState.Equals(LevelState.Current)).First();
        }

        public int GetCurrentLevelIndex()
        {
            for (int i = 0; i < levels.Count; i++)
            {
                if (levels[i].LevelState.Equals(LevelState.Current))
                {
                    return i;
                }
            }
            return 0;
        }

        public int GetLevelIndexByName(string name)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                if (levels[i].LevelName.Equals(name))
                {
                    return i;
                }
            }
            return 0;
        }
    }
}
