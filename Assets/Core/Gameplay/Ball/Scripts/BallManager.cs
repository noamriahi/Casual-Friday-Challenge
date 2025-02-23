using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    public class BallManager : Singleton<BallManager>
    {
        [SerializeField] BallSpawner _ballSpawner;
        [Header("Balls")]
        [SerializeField] int _ballAmount = 60;

        bool _isInGame = false;


        List<Ball> _ballOnBoard = new List<Ball>();


        private void Start()
        {
            Ball.OnDestroyBalls += OnDestroyBallHandler;
            GameEvents.OnGameStart += OnStartGame;
            GameEvents.OnGameEnd += OnEndGame;
        }
        void OnStartGame()
        {
            BallPool.Instance.DestroyBalls(_ballOnBoard.ToArray());
            _ballSpawner.SpawnBalls(_ballAmount, _ballOnBoard);
            _isInGame = true;
        }
        void OnEndGame()
        {
            _isInGame = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _isInGame)
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
                    clickedBall.ExploseBalls();
                }
            }
        }
        void OnDestroyBallHandler(List<Ball> ballToDestroy, Vector3 clickedBallPosition)
        {
            if (!_isInGame) return;
            int ballAmount = ballToDestroy.Count;
            if(ballAmount > 10)
            {
                var specialBall = BallPool.Instance.SpawnSpecialBall(clickedBallPosition);
                _ballOnBoard.Add(specialBall);
            }
            foreach(Ball ball in ballToDestroy)
            {
                _ballOnBoard.Remove(ball);
            }
            _ballSpawner.SpawnBalls(ballAmount, _ballOnBoard);

            if (TapManager.Instance.IsGameOver())
            {
                GameEvents.OnGameEnd?.Invoke();
            }
        }
        public List<Ball> FindConnectedBalls(Ball clickedBall)
        {
            List<Ball> matchedBalls = new List<Ball> { clickedBall };
            RecursiveFind(clickedBall, matchedBalls);
            return matchedBalls;
        }

        private void RecursiveFind(Ball ball, List<Ball> matchedBalls)
        {
            foreach (Ball otherBall in _ballOnBoard)
            {
                if (!matchedBalls.Contains(otherBall) && otherBall.BallType == ball.BallType)
                {
                    float distance = Vector2.Distance(ball.transform.position, otherBall.transform.position);
                    if (distance < 1.2f)
                    {
                        matchedBalls.Add(otherBall);
                        RecursiveFind(otherBall, matchedBalls);
                    }
                }
            }
        }
        public void GetBallAtRadius(List<Ball> matchedBalls, Ball ball, float radius)
        {
            foreach (Ball otherBall in _ballOnBoard)
            {
                float distance = Vector2.Distance(ball.transform.position, otherBall.transform.position);
                if (distance < radius)
                {
                    matchedBalls.Add(otherBall);
                }
            }
        }

        public static int GetScoreCalculateFactor(int ballAmount)
        {
            if (ballAmount <= 10) return 1;
            if (ballAmount <= 20) return 2;
            return 4;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            Ball.OnDestroyBalls -= OnDestroyBallHandler;
            GameEvents.OnGameStart -= OnStartGame;
        }
    }
}
public enum BallType
{
    Red,
    Blue,
    Purple,
    Yellow
}