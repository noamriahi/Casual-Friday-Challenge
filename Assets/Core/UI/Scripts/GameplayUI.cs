using Core.Balls;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [Header("Top UI Texts")]
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _tapText;
    [SerializeField] TMP_Text _targetScoreText;
    [SerializeField] TMP_Text _timerText;

    [Header("Game Popups")]
    [SerializeField] GameObject _startGamePopup;
    [SerializeField] GameObject _endGamePopup;

    [Header("Feedback Objects")]
    [SerializeField] GameObject _notEnoughBalls;

    private Timer _timer;

    void Awake()
    {
        SubscribeToEvents();

        //Initialize UI states
        ToggleStartGamePopup(false);
        ToggleEndGamePopup(false);

    }
    private void SubscribeToEvents()
    {
        ScoreManager.OnScoreUpdate += UpdateScoreUI;
        TapManager.OnTapUpdateEvent += UpdateTapUI;
        Ball.OnPressNoEnoughBalls += ShowNotEnoughBallFeedback;
        GameEvents.OnGameStart += OnGameStart;
        GameEvents.OnGameEnd += OnGameEnd;
    }
    private void UnsubscribeToEvents()
    {
        ScoreManager.OnScoreUpdate -= UpdateScoreUI;
        TapManager.OnTapUpdateEvent -= UpdateTapUI;
        Ball.OnPressNoEnoughBalls -= ShowNotEnoughBallFeedback;
        GameEvents.OnGameStart -= OnGameStart;
        GameEvents.OnGameEnd -= OnGameEnd;
    }
    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }

    void OnGameStart()
    {
        _timer = new Timer()
            .WithTickCallback(UpdateTimerUI)
            .WithEndTimerCallback(OnTimerEnd);
    }
    void OnGameEnd()
    {
        _timer.CancelTimer();
    }
    void UpdateTimerUI(float timeRemaining)
    {
        _timerText.text = timeRemaining.ToTimerFormat();
    }
    void OnTimerEnd()
    {
        GameEvents.OnGameEnd?.Invoke();
    }
    public void CancelTimer()
    {
        _timer.CancelTimer();
    }
    private void Start()
    {
        _targetScoreText.text = ScoreManager.Instance.GetTargetScore().ToString();
    }
    public void ToggleStartGamePopup(bool state)
    {
        _startGamePopup.SetActive(state);
    }
    public void ToggleEndGamePopup(bool state)
    {
        _endGamePopup.SetActive(state);
    }
    private void ShowNotEnoughBallFeedback()
    {
        if (_notEnoughBalls.activeSelf) return;

        _notEnoughBalls.SetActive(true);
        ShowNotEnoughBallFeedbackAsync().Forget();
    }

    private async UniTask ShowNotEnoughBallFeedbackAsync()
    {
        await UniTask.Delay(1000);
        _notEnoughBalls.SetActive(false);
    }
    void UpdateScoreUI(int score)
    {
        _scoreText.text = $"{score}";
    }
    void UpdateTapUI(int tapAmount)
    {
        _tapText.text = $"{tapAmount}";
    }
}
