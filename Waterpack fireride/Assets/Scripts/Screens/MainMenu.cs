using Configs;
using General;
using TMPro;
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
        private SelectLevel selectLevelMenu;

        [SerializeField]
        private Button buttonMusic;

        [SerializeField]
        private Button buttonSound;

        [SerializeField]
        private Button buttonAbout;

        [SerializeField]
        private GeneralSettings generalSettings;

        [SerializeField]
        private LevelsConfig levelsConfig;

        [SerializeField]
        private LevelManager levelManager;

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
            Level currentLevel = levelsConfig.GetCurrentLevel();
            levelManager.LoadLevel(currentLevel);
        }

        private void SelectLevelPress()
        {
            selectLevelMenu.ShowSelectLevelPanel();
        }

        private void MusicPress()
        {
            generalSettings.MusicSwitch = !generalSettings.MusicSwitch;
            buttonMusic.GetComponentInChildren<TextMeshProUGUI>().text =
                "Music\n" + (generalSettings.MusicSwitch ? "ON" : "OFF");
        }

        private void SoundPress()
        {
            generalSettings.SoundSwitch = !generalSettings.SoundSwitch;
            buttonSound.GetComponentInChildren<TextMeshProUGUI>().text =
                "Sound\n" + (generalSettings.SoundSwitch ? "ON" : "OFF");
        }

        private void AboutPress()
        {
            // TODO: implement
        }
    }
}
