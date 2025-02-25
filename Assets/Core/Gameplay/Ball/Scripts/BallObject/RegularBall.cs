using Features.Score;
namespace Core.Balls
{
    /// <summary>
    /// This is the regular ball script, handle the match of a regular ball(more then 3)
    /// </summary>
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

                OnDestroyBalls?.Invoke(connectedBalls,transform.position);
            }
            else
            {
                OnPressNoEnoughBalls?.Invoke();
            }
        }
    }
}