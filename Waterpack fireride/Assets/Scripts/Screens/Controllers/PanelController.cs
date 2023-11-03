using Environment;
using Player;
using Screens;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField]
    private BurningManager burningManager;

    [SerializeField]
    private PlayerCore playerCore;

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

    private void Awake()
    {
        burningManager.OnAllInactive += Win;
        playerCore.OnDeath += Lose;
    }

    public void Lose()
    {
        EndGame();
        //loseScreenMenu.gameObject.SetActive(true);
        loseScreenMenu.OpenScreen();
    }

    public void Win()
    {
        EndGame();
        //winScreenMenu.gameObject.SetActive(true);
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
