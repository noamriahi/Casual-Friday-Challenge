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
    }/*
    public class BallDestroyer : MonoBehaviour
    {
        public void DestroyBalls(List<Ball> ballToDestroy, Vector3 clickedBallPosition, List<Ball> ballOnBoard)
        {
            int ballAmount = ballToDestroy.Count;
            if (ballAmount > 10)
            {
                var specialBall = BallPool.Instance.SpawnSpecialBall(clickedBallPosition);
                ballOnBoard.Add(specialBall);
            }
            foreach (Ball ball in ballToDestroy)
            {
                ballOnBoard.Remove(ball);
            }
        }
    }*/

}
