using Core.Balls;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    public class BallDestroyer : MonoBehaviour
    {
        public void DestroyBalls(List<Ball> ballsToDestroy, Vector3 position)
        {
            foreach (var ball in ballsToDestroy)
            {
                ball.DestroyBall();
            }
        }
    }

}
