using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GeneralSettings", menuName = "Configs/GeneralSettings", order = 0)]
    internal class GeneralSettings : ScriptableObject
    {
        [SerializeField]
        private bool musicSwitch;
        public bool MusicSwitch
        {
            get => musicSwitch;
            set => musicSwitch = value;
        }

        [SerializeField]
        private bool soundSwitch;
        public bool SoundSwitch
        {
            get => soundSwitch;
            set => soundSwitch = value;
        }
    }
}
