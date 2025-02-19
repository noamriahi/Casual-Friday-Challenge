using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{

    public class BallManager : MonoBehaviour
    {
        public static BallManager Instance;

        private List<Ball> allBalls = new List<Ball>();

        private void Awake()
        {
            Instance = this;
        }

        public void RegisterBall(Ball ball)
        {
            if (!allBalls.Contains(ball))
                allBalls.Add(ball);
        }

        void Update()
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
                BallPool.Instance.DestroyBalls(matchedBalls.ToArray());
                foreach (Ball ball in matchedBalls)
                {
                    ball.MarkAsMatched();
                    allBalls.Remove(ball);
                }
            }
        }

        private void FindConnectedBalls(Ball ball, List<Ball> matchedBalls)
        {
            foreach (Ball otherBall in allBalls)
            {
                if (!matchedBalls.Contains(otherBall) && otherBall.BallType == ball.BallType)
                {
                    float distance = Vector2.Distance(ball.transform.position, otherBall.transform.position);
                    if (distance < 1.05f) // Adjust based on your ball size
                    {
                        matchedBalls.Add(otherBall);
                        FindConnectedBalls(otherBall, matchedBalls);
                    }
                }
            }
        }
    }

}