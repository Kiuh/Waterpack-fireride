using Configs;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    [AddComponentMenu("Scripts/Screens.MainMenu")]
    internal class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button buttonPlay;

        [SerializeField]
        private Button buttonSelectLevel;

        [SerializeField]
        private Button buttonMusic;

        [SerializeField]
        private Button buttonSound;

        [SerializeField]
        private Button buttonAbout;

        [SerializeField]
        private GeneralSettings generalSettings;

        private void Awake()
        {
            buttonPlay.onClick.AddListener(PlayPress);
            buttonSelectLevel.onClick.AddListener(SelectLevelPress);
            buttonMusic.onClick.AddListener(MusicPress);
            buttonSound.onClick.AddListener(SoundPress);
            buttonAbout.onClick.AddListener(AboutPress);
        }

        private void PlayPress()
        {
            // TODO: implement
        }

        private void SelectLevelPress()
        {
            // TODO: implement
        }

        private void MusicPress()
        {
            // TODO: implement
        }

        private void SoundPress()
        {
            // TODO: implement
        }

        private void AboutPress()
        {
            // TODO: implement
        }
    }
}
