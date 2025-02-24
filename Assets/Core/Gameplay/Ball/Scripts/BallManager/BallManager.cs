using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    public class BallManager : Singleton<BallManager>
    {
        [SerializeField] BallSpawner _ballSpawner;
        [SerializeField] BallDestroyer _ballDestroyer;


        [Header("Balls")]
        [SerializeField] int _ballAmount = 60;

        bool _isInGame = false;
        bool _canPress = true;


        List<Ball> _ballsOnBoard = new List<Ball>();


        private void Awake()
        {
            Ball.OnDestroyBalls += HandleBallDestruction;
            GameEvents.OnGameStart += OnStartGame;
            GameEvents.OnGameEnd += OnEndGame;
        }
        /// <summary>
        /// On destroy balls, do it on the data from the manager and recreate them again
        /// </summary>
        /// <param name="balls">the balls to destroy ball for the special ball</param>
        /// <param name="position">the position of the </param>

        private void HandleBallDestruction(List<Ball> balls, Vector3 position)
        {
            StartCoroutine(DestroyAndRespawnBalls(balls, position));
        }
        private IEnumerator DestroyAndRespawnBalls(List<Ball> balls, Vector3 position)
        {
            _ballDestroyer.DestroyBalls(balls, position);


            if (TapManager.Instance.IsGameOver())
            {
                GameEvents.OnGameEnd?.Invoke();
            }
            yield return new WaitForSeconds(0.5f);

            _canPress = true;
            _ballsOnBoard.RemoveAll(balls.Contains);

            _ballSpawner.SpawnBalls(balls.Count);

            if (balls.Count >= 10)
            {
                _ballSpawner.SpawnSpecialBall(position);
            }
        }
        public void RegisterBall(Ball ball)
        {
            if (!_ballsOnBoard.Contains(ball))
            {
                _ballsOnBoard.Add(ball);
            }
        }
        void OnStartGame()
        {
            BallPool.Instance.DestroyBalls(_ballsOnBoard.ToArray());
            _ballsOnBoard.Clear();

            _ballSpawner.SpawnBalls(_ballAmount);
            _isInGame = true;
        }
        void OnEndGame()
        {
            _isInGame = false;
        }

        private void Update()
        {
            if (!_canPress) return;
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
                    _canPress = false;

                    clickedBall.Explode();
                }
            }
        }
        /// <summary>
        /// Find the balls connected to the clicked one
        /// </summary>
        /// <param name="clickedBall">The ball the player click on</param>
        /// <returns></returns>
        public List<Ball> FindConnectedBalls(Ball clickedBall)
        {
            var connectedBalls = new List<Ball> { clickedBall };
            RecursiveFind(clickedBall, connectedBalls);
            return connectedBalls;
        }

        private void RecursiveFind(Ball ball, List<Ball> matchedBalls)
        {
            foreach (Ball otherBall in _ballsOnBoard)
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
            foreach (Ball otherBall in _ballsOnBoard)
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
            Ball.OnDestroyBalls -= HandleBallDestruction;
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