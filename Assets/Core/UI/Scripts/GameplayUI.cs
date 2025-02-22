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

    bool isFeedbackActive = false;

    void Awake()
    {
        ScoreManager.OnScoreUpdate += OnScoreUpdateHandle;
        TapManager.OnTapUpdateEvent += OnTapUpdateHandle;
        Ball.OnPressNoEnoughBalls += ShowNotEnoughBallFeedback;
        ToggleStartGamePopup(false);
        ToggleEndGamePopup(false);
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
    async void ShowNotEnoughBallFeedback()
    {
        if (isFeedbackActive) return;
        isFeedbackActive = true;
        
        _notEnoughBalls.gameObject.SetActive(true);
        await UniTask.WaitForSeconds(1f);
        _notEnoughBalls.gameObject.SetActive(false);
        isFeedbackActive = false;
    }
    void OnScoreUpdateHandle(int score)
    {
        _scoreText.text = $"{score}";
    }
    void OnTapUpdateHandle(int tapAmount)
    {
        _tapText.text = $"{tapAmount}";
    }
    private void OnDestroy()
    {
        ScoreManager.OnScoreUpdate -= OnScoreUpdateHandle;
        TapManager.OnTapUpdateEvent -= OnTapUpdateHandle;
    }
}
