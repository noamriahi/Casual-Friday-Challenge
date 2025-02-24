using Core.Addressable;
using Core.Balls;
using Core.Popups;
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

    [Header("Feedback Objects")]
    [SerializeField] GameObject _notEnoughBalls;

    GameObject _startPopup;

    private Timer _timer;
    private int _gameTime;

    void Awake()
    {
        SubscribeToEvents();
    }
    public void Initialize(int gameTime)
    {
        _gameTime = gameTime;
        _scoreSlide.maxValue = ScoreManager.Instance.GetTargetScore();
        UpdateTimerUI(_gameTime);
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
        _timer = new Timer(_gameTime)
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
    public void ShowStartGamePopup(bool state)
    {
        if(state)
        {
            new LoadAssetCommand("Popups/StartGamePopup")
                .WithLoadCallback(LoadPopup)
                .Execute();
        }
        else
        {
            Destroy(_startPopup);
        }
    }
    public void ShowEndGamePopup()
    {
        new LoadAssetCommand("Popups/EndGamePopup")
            .WithLoadCallback(LoadPopup)
            .Execute();
    }
    void LoadPopup(GameObject popup)
    {
        var popupObeject = popup.GetComponent<Popup>();
        PopupManager.Instance.OpenPopup(popupObeject);
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
        var targetScore = ScoreManager.Instance.GetTargetScore();
        _scoreText.text = $"{score}/{targetScore}";
        _scoreSlide.value = score;
    }
    void UpdateTapUI(int tapAmount)
    {
        _tapText.text = $"{tapAmount}";
    }
}
