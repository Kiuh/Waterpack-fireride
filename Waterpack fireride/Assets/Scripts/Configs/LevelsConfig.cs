using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/LevelsConfig", order = 1)]
    internal class LevelsConfig : ScriptableObject
    {
        [SerializeField]
        private List<Level> levels;
        public List<Level> Levels => levels;
    }
}
