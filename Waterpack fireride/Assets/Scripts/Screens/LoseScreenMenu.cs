using Configs;
using General;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    public class LoseScreenMenu : MonoBehaviour
    {
        [SerializeField]
        private LevelManager levelManager;

        [SerializeField]
        private LevelsConfig levelsConfig;

        [SerializeField]
        private TextMeshProUGUI levelNameText;

        [SerializeField]
        private Button mainMenuButton;

        [SerializeField]
        private Button restartButton;

        [Header("Animation settings")]
        [Space]
        [SerializeField]
        private float maxPanelPositionY;

        [SerializeField]
        private float animationSpeed;

        private RectTransform loseScreenMenuRectTransform;

        private void Start()
        {
            loseScreenMenuRectTransform = gameObject.GetComponent<RectTransform>();
            UpdateLevelNameText();
            mainMenuButton.onClick.AddListener(MainMenuPress);
            restartButton.onClick.AddListener(RestartPress);
        }

        private void MainMenuPress()
        {
            levelManager.LoadMainScene();
        }

        private void RestartPress()
        {
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
            _ = LeanTween
                .moveY(loseScreenMenuRectTransform, maxPanelPositionY, animationSpeed)
                .setIgnoreTimeScale(true);
        }
    }
}
