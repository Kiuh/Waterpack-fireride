using Configs;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    [AddComponentMenu("Scripts/Screens.SelectLevel")]
    internal class SelectLevel : MonoBehaviour
    {
        [SerializeField]
        private LevelsConfig levelsConfig;

        [SerializeField]
        private Button clickableBackground;

        [SerializeField]
        private Button playSelected;

        private void Awake()
        {
            // TODO: fill from config
        }

        public void ShowSelectLevelPanel() { }

        public void HideSelectLevelPanel() { }
    }
}
