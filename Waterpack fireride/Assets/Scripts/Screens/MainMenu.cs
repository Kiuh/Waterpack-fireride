using Configs;
using General;
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

        private StateButtonController soundStateButtonController;

        private StateButtonController musicStateButtonController;

        private void Start()
        {
            soundStateButtonController =
                buttonSound.gameObject.GetComponent<StateButtonController>();
            musicStateButtonController =
                buttonMusic.gameObject.GetComponent<StateButtonController>();

            buttonPlay.onClick.AddListener(PlayPress);
            buttonSelectLevel.onClick.AddListener(SelectLevelPress);
            buttonMusic.onClick.AddListener(MusicPress);
            buttonSound.onClick.AddListener(SoundPress);
            buttonAbout.onClick.AddListener(AboutPress);
            levelManager.CurrentLevel = levelsConfig.GetCurrentLevel();
        }

        private void PlayPress()
        {
            Time.timeScale = 1;
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
            musicStateButtonController.SwitchSprite();
        }

        private void SoundPress()
        {
            generalSettings.SoundSwitch = !generalSettings.SoundSwitch;
            soundStateButtonController.SwitchSprite();
        }

        private void AboutPress()
        {
            // TODO: implement
        }
    }
}
