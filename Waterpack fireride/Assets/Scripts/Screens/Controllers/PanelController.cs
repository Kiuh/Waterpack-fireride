using Screens;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField]
    private WinScreenMenu winScreenMenu;

    [SerializeField]
    private LoseScreenMenu loseScreenMenu;

    [SerializeField]
    private RectTransform playTimer;

    [SerializeField]
    private RectTransform pauseButton;

    [Header("Animation settings for timer")]
    [Space]
    [SerializeField]
    private float maxTimerPositionY;

    [SerializeField]
    private float timerAnimationSpeed;

    [Header("Animation settings for pause button")]
    [Space]
    [SerializeField]
    private float maxPauseButtonPositionY;

    [SerializeField]
    private float pauseButtonAnimationSpeed;

    public void Lose()
    {
        EndGame();
        loseScreenMenu.OpenScreen();
    }

    public void Win()
    {
        EndGame();
        winScreenMenu.OpenScreen();
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        _ = LeanTween
            .moveX(playTimer, maxTimerPositionY, timerAnimationSpeed)
            .setIgnoreTimeScale(true);
        _ = LeanTween
            .moveX(pauseButton, maxPauseButtonPositionY, pauseButtonAnimationSpeed)
            .setIgnoreTimeScale(true);
    }
}
