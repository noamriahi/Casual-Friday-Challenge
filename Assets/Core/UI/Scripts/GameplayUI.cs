using Core.Addressable;
using Core.Balls;
using Core.Popups;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    /// <summary>
    /// This is the main UI script of the gameplay, I use the MVP pattern(of course no need for model)
    /// This script is the "View" and there is a presenter that handle the logic.
    /// </summary>
    public class GameplayUI : MonoBehaviour
    {
        [Header("Top UI Texts")]
        [SerializeField] TMP_Text _tapText;
        [SerializeField] TMP_Text _timerText;

        [Header("ScoreUI")]
        [SerializeField] Slider _scoreSlider;
        [SerializeField] TMP_Text _scoreText;

        [Header("Feedback Objects")]
        [SerializeField] GameObject _notEnoughBalls;


        private GamePresenter _presenter;


        internal int _gameTime;

        private void Awake()
        {
            _presenter = new GamePresenter(this);
        }
        private void OnDestroy()
        {
            _presenter.Dispose();
        }
        public void Initialize(int gameTime)
        {
            _gameTime = gameTime;
            SetTimerText(gameTime);
            _scoreSlider.maxValue = ScoreManager.Instance.GetTargetScore();
        }
        public void SetTimerText(float time) => _timerText.text = time.ToTimerFormat();
        public void ShowNotEnoughBallsFeedback()
        {
            if (_notEnoughBalls.activeSelf) return;
            _notEnoughBalls.SetActive(true);
            HideNotEnoughBallsAfterDelay().Forget();
        }
        private async UniTask HideNotEnoughBallsAfterDelay()
        {
            await UniTask.Delay(1000);
            _notEnoughBalls.SetActive(false);
        }
        public void SetScore(int score, int targetScore, bool reachScore = false)
        {
            _scoreText.text = $"{score}/{targetScore}";
            _scoreSlider.value = score;
            if (reachScore) 
            { 
                _scoreSlider.GetComponent<RectTransform>().BounceScaleUI(1f);
            }

        }
        internal void SetTapAmount(int tapAmount) => _tapText.text = tapAmount.ToString();
    }

}