using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    public class BallPool : MonoBehaviour
    {
        [Header("Spawn Configs")]
        [SerializeField] private Ball _ballPrefab;
        [SerializeField] private Transform _ballParent;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private int _maxBalls = 60;
        [SerializeField] private float _spawnDelay = 0.1f;
        [SerializeField] private List<BallConfigSO> _ballTypes; 

        private Queue<Ball> _inactiveBalls = new Queue<Ball>();
        private List<Ball> _activeBalls = new List<Ball>();

        public static BallPool Instance;
        private void Awake() => Instance = this;

        private void Start()
        {
            StartCoroutine(SpawnBallsCoroutine());
        }

        private IEnumerator SpawnBallsCoroutine()
        {
            while (_activeBalls.Count < _maxBalls)
            {
                SpawnBall();
                yield return new WaitForSeconds(_spawnDelay);
            }
        }

        public Ball SpawnBall()
        {
            Ball newBall;
            if (_inactiveBalls.Count > 0)
            {
                newBall = _inactiveBalls.Dequeue();
                newBall.gameObject.SetActive(true);
            }
            else
            {
                newBall = Instantiate(_ballPrefab, _spawnPoint.position, Quaternion.identity, _ballParent);
            }

            AssignRandomType(newBall);
            _activeBalls.Add(newBall);
            return newBall;
        }

        public void DestroyBalls(List<Ball> matchedBalls)
        {
            foreach (var ball in matchedBalls)
            {
                ball.transform.position = _spawnPoint.position;
                ball.MarkAsMatched();
                ball.gameObject.SetActive(false);
                _activeBalls.Remove(ball);
                _inactiveBalls.Enqueue(ball);
            }

            StartCoroutine(SpawnBallsCoroutine()); // Refill balls
        }

        public List<Ball> GetActiveBalls()
        {
            return _activeBalls;
        }

        private void AssignRandomType(Ball ball)
        {
            BallConfigSO data = _ballTypes[Random.Range(0, _ballTypes.Count)];
            ball.SetType(data);
        }
    }
}
