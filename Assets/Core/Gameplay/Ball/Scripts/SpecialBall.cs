using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    public class SpecialBall : Ball
    {
        [Header("Special Ball")]
        [SerializeField] float _explosionRadius = 2f;
        public override void ExploseBalls()
        {
            TapManager.Instance.TapBall();
            List<Ball> ballAtRadius = new List<Ball>();
            BallManager.Instance.GetBallAtRadius(ballAtRadius, this, _explosionRadius);
            foreach(Ball ball in ballAtRadius)
            {
                ball.DestroyBall();
            }
        }
    }
}
