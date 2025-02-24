using Features.Score;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    public class SpecialBall : Ball
    {
        [Header("Special Ball")]
        [SerializeField] float _explosionRadius = 2f;
        public override void Explode()
        {
            TapManager.Instance.TapBall();
            List<Ball> ballAtRadius = new List<Ball>();
            BallManager.Instance.GetBallAtRadius(ballAtRadius, this, _explosionRadius);

            var scoreFactor = BallManager.GetScoreCalculateFactor(ballAtRadius.Count);
            new UpdateScoreCommand(ballAtRadius.Count * scoreFactor).Execute();
            OnDestroyBalls?.Invoke(ballAtRadius, transform.position);
        }
    }
}
