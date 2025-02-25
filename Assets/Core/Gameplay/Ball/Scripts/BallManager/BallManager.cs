using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    /// <summary>
    /// This is the scripts that manage the ball system.
    /// It's create balls, destroy them, save the ball list and find other ball for explostion logics.
    /// </summary>
    public class BallManager : Singleton<BallManager>
    {
        [Header("Ball componenets")]
        [SerializeField] private BallSpawner _ballSpawner;
        [SerializeField] private BallDestroyer _ballDestroyer;


        [Header("Balls")]
        [SerializeField] private int _ballAmount = 60;

        private bool _isInGame = false;
        private bool _canPress = true;


        //I use hash table for better performance on the search
        private HashSet<Ball> _ballsOnBoard = new HashSet<Ball>();


        private void Awake()
        {
            Ball.OnDestroyBalls += HandleBallDestruction;
            GameEvents.OnGameStart += OnStartGame;
            GameEvents.OnGameEnd += OnEndGame;
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            Ball.OnDestroyBalls -= HandleBallDestruction;
            GameEvents.OnGameStart -= OnStartGame;
            GameEvents.OnGameEnd -= OnEndGame;
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
            _canPress = false;
            _ballDestroyer.DestroyBalls(balls, position);
            yield return new WaitForSeconds(0.5f);

            foreach (var ball in balls)
            {
                _ballsOnBoard.Remove(ball);
            }

            _ballSpawner.SpawnBalls(balls.Count);

            if (balls.Count >= 10)
            {
                _ballSpawner.SpawnSpecialBall(position);
            }

            if (TapManager.Instance.IsGameOver())
            {
                GameEvents.OnGameEnd?.Invoke();
            }
            _canPress = true;
        }
        public void RegisterBall(Ball ball)
        {
            _ballsOnBoard.Add(ball);
        }
        void OnStartGame()
        {
            BallPool.Instance.DestroyBalls(_ballsOnBoard);
            _ballsOnBoard.Clear();

            _ballSpawner.SpawnBalls(_ballAmount);
            _canPress = true;
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
            var connectedBalls = new HashSet<Ball> { clickedBall };
            RecursiveFind(clickedBall, connectedBalls);
            return new List<Ball>(connectedBalls);
        }

        private void RecursiveFind(Ball ball, HashSet<Ball> matchedBalls)
        {
            foreach (Ball otherBall in _ballsOnBoard)
            {
                if (matchedBalls.Contains(otherBall)) continue;
                if (otherBall.BallType != ball.BallType) continue;

                float distance = Vector2.Distance(ball.transform.position, otherBall.transform.position);
                if (distance < 1.2f)
                {
                    matchedBalls.Add(otherBall);
                    RecursiveFind(otherBall, matchedBalls);
                }
            }
        }
        public void GetBallAtRadius(List<Ball> matchedBalls, Ball ball, float radius)
        {
            foreach (Ball otherBall in _ballsOnBoard)
            {
                if (otherBall == ball || matchedBalls.Contains(otherBall)) continue;

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

    }
}
public enum BallType
{
    Red,
    Blue,
    Purple,
    Yellow
}