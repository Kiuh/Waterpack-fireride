using Configs;
using General;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    public class WinScreenMenu : MonoBehaviour
    {
        [SerializeField]
        private LevelManager levelManager;

        [SerializeField]
        private LevelsConfig levelsConfig;

        [SerializeField]
        private TextMeshProUGUI levelNameText;

        [SerializeField]
        private TextMeshProUGUI levelPlayTime;

        [SerializeField]
        private Button mainMenuButton;

        [SerializeField]
        private Button restartButton;

        [SerializeField]
        private Button nextLevelButton;

        [SerializeField]
        private TextMeshProUGUI timerText;

        [Header("Animation settings")]
        [Space]
        [SerializeField]
        private float maxPanelPositionY;

        [SerializeField]
        private float animationSpeed;

        private RectTransform winScreenMenuRectTransform;

        private void Start()
        {
            winScreenMenuRectTransform = gameObject.GetComponent<RectTransform>();
            UpdateLevelNameText();
            nextLevelButton.onClick.AddListener(NextLevelPress);
            mainMenuButton.onClick.AddListener(MainMenuPress);
            restartButton.onClick.AddListener(RestartPress);
        }

        private void MainMenuPress()
        {
            Time.timeScale = 1;
            levelManager.LoadMainScene();
        }

        private void NextLevelPress()
        {
            Time.timeScale = 1;
            Level currentLevel = levelsConfig.GetCurrentLevel();
            levelManager.LoadLevel(currentLevel);
        }

        private void RestartPress()
        {
            Time.timeScale = 1;
            levelManager.RestartCurrentLevel();
        }

        private void UpdateLevelNameText()
        {
            levelNameText.text =
                (levelsConfig.GetCurrentLevelIndex() + 1).ToString()
                + ". "
                + levelManager.CurrentLevel.LevelName;
        }

        public void OpenScreen()
        {
            levelPlayTime.text = timerText.text;
            _ = LeanTween
                .moveY(winScreenMenuRectTransform, maxPanelPositionY, animationSpeed)
                .setIgnoreTimeScale(true);
            int index = levelsConfig.GetCurrentLevelIndex();

            List<Level> levels = levelsConfig.Levels;

            levels[index].LevelState = LevelState.Passed;
            levels[index + 1].LevelState = LevelState.Current;
        }
    }
}
