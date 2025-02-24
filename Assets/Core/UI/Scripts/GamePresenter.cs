using Core.Balls;

namespace Core.UI
{

    public class GamePresenter
    {
        private readonly GameplayUI _ui;
        private Timer _timer;

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
            _ui.SetScore(score, ScoreManager.Instance.GetTargetScore());
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