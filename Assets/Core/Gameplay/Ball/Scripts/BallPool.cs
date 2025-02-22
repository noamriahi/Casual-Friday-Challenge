using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    public class BallPool : Singleton<BallPool>
    {
        [Header("Regualr Balls")]
        [SerializeField] private RegularBall _regularBallPrefab;
        [SerializeField] private Transform _ballParent;
        [SerializeField] private int _amountToPool = 60;
        [SerializeField] private List<BallConfigSO> _ballTypes;

        [Header("Special Balls")]
        [SerializeField] private SpecialBall _specialBallPrefab;
        [SerializeField] private int _amountOfSpecialToPool = 5;

        Queue<RegularBall> _ballQueue = new Queue<RegularBall>();
        Queue<SpecialBall> _specialsBallQueue = new Queue<SpecialBall>();


        private void Start()
        {
            for (int i = 0; i < _amountToPool; i++)
            {
                var newBall = Instantiate(_regularBallPrefab, _ballParent);
                newBall.gameObject.SetActive(false);
                _ballQueue.Enqueue(newBall);
            }
            for (int i = 0; i < _amountOfSpecialToPool; i++)
            {
                var newBall = Instantiate(_specialBallPrefab, _ballParent);
                newBall.gameObject.SetActive(false);
                _specialsBallQueue.Enqueue(newBall);
            }
        }


        public RegularBall CreateRegularBall(Vector3 position)
        {
            RegularBall newBall = GetOrCreateBall();
            AssignRandomType(newBall);
            newBall.transform.position = position;
            return newBall;
        }

        public SpecialBall SpawnSpecialBall(Vector3 position)
        {
            SpecialBall specialBall = GetOrCreateSpecialBall();
            specialBall.transform.position = position;
            return specialBall;
        }

        private void AssignRandomType(Ball ball)
        {
            BallConfigSO data = _ballTypes[Random.Range(0, _ballTypes.Count)];
            ball.SetType(data);
        }
        private RegularBall GetOrCreateBall()
        {
            if(_ballQueue == null || _ballQueue.Count == 0)
            {
                var newBall = Instantiate(_regularBallPrefab, _ballParent);
                newBall.gameObject.SetActive(true);
                return newBall;
            }
            var ball = _ballQueue.Dequeue();
            ball.gameObject.SetActive(true);
            return ball;
        }        
        private SpecialBall GetOrCreateSpecialBall()
        {
            if (_specialsBallQueue == null || _specialsBallQueue.Count == 0)
            {
                var newBall = Instantiate(_specialBallPrefab, _ballParent);
                newBall.gameObject.SetActive(true);
                return newBall;
            }
            var ball = _specialsBallQueue.Dequeue();
            ball.gameObject.SetActive(true);
            return ball;
        }

        public void DestroyBalls(params Ball[] balls)
        {
            foreach (var ball in balls)
            {
                ball.gameObject.SetActive(false);
                if(ball is SpecialBall specialBall)
                {
                    _specialsBallQueue.Enqueue(specialBall);
                }
                else
                {
                    _ballQueue.Enqueue((RegularBall)ball);
                }
            }
        }
    }
}
