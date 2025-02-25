using Core.Balls;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    /// <summary>
    /// To work in a SOLID way this script handle only the destroy of balls.
    /// </summary>
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
