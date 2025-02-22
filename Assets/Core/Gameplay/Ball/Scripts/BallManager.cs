using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    public class BallManager : MonoBehaviour
    {
        [Header("Balls")]
        [SerializeField] Transform _ballSpawnPoint;
        [SerializeField] float _spawnDelay = 0.05f;
        [SerializeField] int _ballAmount = 60;

        bool _isInGame = false;

        public static BallManager Instance;

        List<Ball> _ballOnBoard = new List<Ball>();

        private void Awake() => Instance = this;

        private void Start()
        {
            Ball.OnDestroyBalls += OnDestroyBallHandler;
            GameEvents.OnGameStart += OnStartGame;
            GameEvents.OnGameEnd += OnEndGame;
        }
        void OnStartGame()
        {
            BallPool.Instance.DestroyBalls(_ballOnBoard.ToArray());
            StartCoroutine(SpawnBalls(_ballAmount));
            _isInGame = true;
        }
        void OnEndGame()
        {
            _isInGame = false;
        }
        IEnumerator SpawnBalls(int ballAmount)
        {
            for (int i = 0; i < ballAmount; i++)
            {
                var ball = BallPool.Instance.CreateRegularBall(_ballSpawnPoint.position);
                _ballOnBoard.Add(ball);
                yield return new WaitForSeconds(_spawnDelay);
            }
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
            StartCoroutine(SpawnBalls(ballAmount));

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
        private void OnDestroy()
        {
            Instance = null;

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