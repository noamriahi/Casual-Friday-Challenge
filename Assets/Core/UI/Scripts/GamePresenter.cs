using Core.Balls;

namespace Core.UI
{
    /// <summary>
    /// This is the Presenter of the GameplayUI script that handle the logic.
    /// It's implement the MVP pattern.
    /// </summary>
    public class GamePresenter
    {
        private readonly GameplayUI _ui;
        private Timer _timer;

        bool _reachTargetScore = false;
        public GamePresenter(GameplayUI ui)
        {
            _ui = ui;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            ScoreManager.OnScoreUpdate += UpdateScore;
            TapManager.OnTapUpdateEvent += UpdateTap;
            Ball.OnPressNoEnoughBalls += _ui.ShowNotEnoughBallsFeedback;
            GameEvents.OnGameStart += StartGame;
            GameEvents.OnGameEnd += EndGame;
        }

        private void UnsubscribeToEvents()
        {
            ScoreManager.OnScoreUpdate -= UpdateScore;
            TapManager.OnTapUpdateEvent -= UpdateTap;
            Ball.OnPressNoEnoughBalls -= _ui.ShowNotEnoughBallsFeedback;
            GameEvents.OnGameStart -= StartGame;
            GameEvents.OnGameEnd -= EndGame;
        }

        private void StartGame()
        {
            _reachTargetScore = false;
            _timer = new Timer(_ui._gameTime)
                .WithTickCallback(_ui.SetTimerText)
                .WithEndTimerCallback(OnTimerEnd);
        }

        private void EndGame()
        {
            _timer.CancelTimer();
        }
        void OnTimerEnd()
        {
            GameEvents.OnGameEnd?.Invoke();
        }

        private void UpdateScore(int score)
        {
            var targetScore = ScoreManager.Instance.GetTargetScore();
            
            if(!_reachTargetScore && score >= targetScore)
            {
                _reachTargetScore = true;
                _ui.SetScore(score, targetScore, true);
                return;
            }
            _ui.SetScore(score, targetScore);
        }

        private void UpdateTap(int tapAmount)
        {
            _ui.SetTapAmount(tapAmount);
        }

        public void Dispose()
        {
            UnsubscribeToEvents();
        }
    }

}