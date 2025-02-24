using Core.Balls;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [Header("Top UI Texts")]
    [SerializeField] TMP_Text _tapText;
    [SerializeField] TMP_Text _timerText;

    [Header("ScoreUI")]
    [SerializeField] Slider _scoreSlide;
    [SerializeField] TMP_Text _scoreText;
    [Header("Game Popups")]
    [SerializeField] GameObject _startGamePopup;

    [SerializeField] Canvas _canvas;

    [Header("Feedback Objects")]
    [SerializeField] GameObject _notEnoughBalls;

    private Timer _timer;

    private int _targetScore;

    void Awake()
    {
        SubscribeToEvents();

        //Initialize UI states
        ToggleStartGamePopup(false);

    }
    private void Start()
    {
        _targetScore = ScoreManager.Instance.GetTargetScore();
        _scoreSlide.maxValue = _targetScore;
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
    public void ToggleStartGamePopup(bool state)
    {
        _startGamePopup.SetActive(state);
    }
    public void ShowEndGamePopup()
    {
        new LoadAssetCommand("Popups/EndGamePopup")
            .WithLoadCallback(LoadEndGamePopup)
            .Execute();
    }
    void LoadEndGamePopup(GameObject popup)
    {
        Instantiate(popup, _canvas.transform);
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
        _scoreText.text = $"{score}/{_targetScore}";
        _scoreSlide.value = score;
    }
    void UpdateTapUI(int tapAmount)
    {
        _tapText.text = $"{tapAmount}";
    }
}
