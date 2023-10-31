using Configs;
using General;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    public class GameSceneMenu : MonoBehaviour
    {
        [SerializeField]
        private LevelManager levelManager;

        [SerializeField]
        private GeneralSettings generalSettings;

        [SerializeField]
        private Button musicButton;

        [SerializeField]
        private Button soundButton;

        [SerializeField]
        private Button aboutButton;

        [SerializeField]
        private Button mainMenuButton;

        [SerializeField]
        private Button restartButton;

        [SerializeField]
        private Button continueButton;

        [SerializeField]
        private Button pauseButton;

        [SerializeField]
        private GameObject pausePanelGO;

        [SerializeField]
        private RectTransform pauseMenuButtonsRectTransform;

        [SerializeField]
        private float pausePanelMaxAlpha;

        [SerializeField]
        private float pauseMenuButtonsMinPositionY;

        [SerializeField]
        private float pauseMenuButtonsMaxPositionY;

        [SerializeField]
        private float pauseButtonMinPositionX;

        [SerializeField]
        private float pauseButtonMaxPositionX;

        [SerializeField]
        private float animationSpeed;

        private RectTransform pauseButtonRectTransform;

        private CanvasGroup pausePanelCanvasGroup;

        private Image pausePanelImage;

        private void Awake()
        {
            pausePanelCanvasGroup = pausePanelGO.GetComponent<CanvasGroup>();
            pausePanelImage = pausePanelGO.GetComponent<Image>();
            pauseButtonRectTransform = pauseButton.GetComponent<RectTransform>();

            musicButton.onClick.AddListener(MusicPress);
            soundButton.onClick.AddListener(SoundPress);
            aboutButton.onClick.AddListener(AboutPress);
            mainMenuButton.onClick.AddListener(MainMenuPress);
            restartButton.onClick.AddListener(RestartPress);
            continueButton.onClick.AddListener(ContinuePress);
            pauseButton.onClick.AddListener(PausePress);
        }

        private void MusicPress()
        {
            generalSettings.MusicSwitch = !generalSettings.MusicSwitch;
            musicButton.GetComponentInChildren<TextMeshProUGUI>().text =
                "Music\n" + (generalSettings.MusicSwitch ? "ON" : "OFF");
        }

        private void SoundPress()
        {
            generalSettings.SoundSwitch = !generalSettings.SoundSwitch;
            soundButton.GetComponentInChildren<TextMeshProUGUI>().text =
                "Sound\n" + (generalSettings.SoundSwitch ? "ON" : "OFF");
        }

        private void AboutPress()
        {
            // TODO: implement
        }

        private void MainMenuPress()
        {
            levelManager.LoadMainScene();
        }

        private void RestartPress()
        {
            levelManager.RestartCurrentLevel();
        }

        private void ContinuePress()
        {
            pausePanelCanvasGroup.blocksRaycasts = false;
            _ = LeanTween.value(
                gameObject,
                UpdatePausePanelAlphaCallback,
                pausePanelMaxAlpha,
                0,
                animationSpeed
            );
            _ = LeanTween.moveX(pauseButtonRectTransform, pauseButtonMaxPositionX, animationSpeed);
            _ = LeanTween.moveY(
                pauseMenuButtonsRectTransform,
                pauseMenuButtonsMinPositionY,
                animationSpeed
            );
        }

        private void PausePress()
        {
            pausePanelCanvasGroup.blocksRaycasts = true;
            _ = LeanTween.value(
                gameObject,
                UpdatePausePanelAlphaCallback,
                0,
                pausePanelMaxAlpha,
                animationSpeed
            );
            _ = LeanTween.moveX(pauseButtonRectTransform, pauseButtonMinPositionX, animationSpeed);
            _ = LeanTween.moveY(
                pauseMenuButtonsRectTransform,
                pauseMenuButtonsMaxPositionY,
                animationSpeed
            );
        }

        private void UpdatePausePanelAlphaCallback(float alpha)
        {
            pausePanelImage.color = new Color(
                pausePanelImage.color.r,
                pausePanelImage.color.g,
                pausePanelImage.color.b,
                alpha
            );
        }
    }
}
