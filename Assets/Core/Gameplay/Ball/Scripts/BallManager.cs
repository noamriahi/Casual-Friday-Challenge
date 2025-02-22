using Features.Score;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    public class BallManager : MonoBehaviour
    {
        public static BallManager Instance;
        private int _specialBallCounter = 0;

        private void Awake() => Instance = this;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                DetectClickedBall();
            }
        }

        private void DetectClickedBall()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                Ball clickedBall = hit.collider.GetComponent<Ball>();
                if (clickedBall != null)
                {
                    CheckMatch(clickedBall);
                }
            }
        }

        public void CheckMatch(Ball clickedBall)
        {
            List<Ball> matchedBalls = new List<Ball> { clickedBall };
            FindConnectedBalls(clickedBall, matchedBalls);

            if (matchedBalls.Count >= 3)
            {
                BallPool.Instance.DestroyBalls(matchedBalls);
                var factor = GetScoreCalculateFactor(matchedBalls.Count);
                new UpdateScoreCommand(matchedBalls.Count * factor).Execute();

                _specialBallCounter += matchedBalls.Count;

                if (_specialBallCounter >= 10)
                {
                    SpawnSpecialBall();
                    _specialBallCounter = 0;
                }
                TapManager.Instance.TapBall();
            }
            else
            {
                //GameManager.Instance.ShowMissText(clickedBall.transform.position);
            }
        }

        private void FindConnectedBalls(Ball ball, List<Ball> matchedBalls)
        {
            List<Ball> allBalls = BallPool.Instance.GetActiveBalls(); // Ask Pool for active balls

            foreach (Ball otherBall in allBalls)
            {
                if (!matchedBalls.Contains(otherBall) && otherBall.BallType == ball.BallType)
                {
                    float distance = Vector2.Distance(ball.transform.position, otherBall.transform.position);
                    if (distance < 1.2f)
                    {
                        matchedBalls.Add(otherBall);
                        FindConnectedBalls(otherBall, matchedBalls);
                    }
                }
            }
        }

        private void SpawnSpecialBall()
        {
            //Ball specialBall = BallPool.Instance.SpawnBall();
            //specialBall.SetType(SpecialBallData.Instance);
        }
        private static int GetScoreCalculateFactor(int ballAmount)
        {
            if (ballAmount <= 10)
                return 1;
            if (ballAmount <= 20)
                return 2;
            return 4;
        }
    }
    public enum BallType
    {
        Red,
        Blue,
        Purple,
        Yellow
    }
}
