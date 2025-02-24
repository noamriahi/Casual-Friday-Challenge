using Features.Score;
namespace Core.Balls
{
    public class RegularBall : Ball
    {
        public override void Explode()
        {
            var connectedBalls = BallManager.Instance.FindConnectedBalls(this);

            if (connectedBalls.Count >= 3)
            {
                TapManager.Instance.TapBall();
                var scoreFactor = BallManager.GetScoreCalculateFactor(connectedBalls.Count);
                new UpdateScoreCommand(connectedBalls.Count * scoreFactor).Execute();

                foreach (var ball in connectedBalls)
                {
                    ball.DestroyBall();
                }
                OnDestroyBalls?.Invoke(connectedBalls,transform.position);
            }
            else
            {
                OnPressNoEnoughBalls?.Invoke();
            }
        }
    }
}