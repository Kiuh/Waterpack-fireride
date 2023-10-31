using Common;
using System;
using UnityEngine;

namespace Configs
{
    internal enum LevelState
    {
        Passed = 0,
        Current = 1,
        NotPassed = 2,
    }

    [Serializable]
    internal class Level
    {
        [SerializeField]
        private string levelName;
        public string LevelName => levelName;

        [Scene]
        [SerializeField]
        private string sceneName;
        public string SceneName => sceneName;

        [SerializeField]
        [InspectorReadOnly]
        private float bestTime;
        public float BestTime
        {
            get => bestTime;
            set => bestTime = value;
        }

        [SerializeField]
        private LevelState levelState = LevelState.NotPassed;
        public LevelState LevelState
        {
            get => levelState;
            set => levelState = value;
        }
    }
}
