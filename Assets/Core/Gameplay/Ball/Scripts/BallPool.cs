using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Core.Balls
{
    /// <summary>
    /// The ball pool manager a pool of balls(regular and special) to avoid too much work of the GC.
    /// </summary>
    public class BallPool : Singleton<BallPool>
    {
        [Header("Regular Balls")]
        [SerializeField] private RegularBall _regularBallPrefab;
        [SerializeField] private Transform _ballParent;
        [SerializeField] private int _amountToPool = 60;
        [SerializeField] private List<BallConfigSO> _ballTypes;

        [Header("Special Balls")]
        [SerializeField] private SpecialBall _specialBallPrefab;
        [SerializeField] private int _amountOfSpecialToPool = 5;

        private ObjectPool<RegularBall> _regularBallPool;
        private ObjectPool<SpecialBall> _specialBallPool;

        private void Awake()
        {
            _regularBallPool = new ObjectPool<RegularBall>(
                createFunc: () => Instantiate(_regularBallPrefab, _ballParent),
                actionOnGet: ball => ball.gameObject.SetActive(true),
                actionOnRelease: ball => ball.gameObject.SetActive(false),
                collectionCheck: false,
                defaultCapacity: _amountToPool
            );

            _specialBallPool = new ObjectPool<SpecialBall>(
                createFunc: () => Instantiate(_specialBallPrefab, _ballParent),
                actionOnGet: ball => ball.gameObject.SetActive(true),
                actionOnRelease: ball => ball.gameObject.SetActive(false),
                collectionCheck: false,
                defaultCapacity: _amountOfSpecialToPool
            );

            // Prepopulate the pools
            for (int i = 0; i < _amountToPool; i++)
                _regularBallPool.Release(_regularBallPool.Get());

            for (int i = 0; i < _amountOfSpecialToPool; i++)
                _specialBallPool.Release(_specialBallPool.Get());
        }

        /// <summary>
        /// Creates a regular ball at a given position.
        /// </summary>
        public RegularBall CreateRegularBall(Vector3 position)
        {
            RegularBall newBall = GetOrCreateBall();
            AssignRandomType(newBall);
            newBall.transform.position = position;
            return newBall;
        }

        /// <summary>
        /// Spawns a special ball at the given position.
        /// </summary>
        public SpecialBall SpawnSpecialBall(Vector3 position)
        {
            SpecialBall specialBall = GetOrCreateSpecialBall();
            specialBall.transform.position = position;
            return specialBall;
        }

        private void AssignRandomType(Ball ball)
        {
            BallConfigSO data = _ballTypes[Random.Range(0, _ballTypes.Count)];
            ball.Initialize(data);
        }

        private RegularBall GetOrCreateBall()
        {
            return _regularBallPool.Get();
        }

        private SpecialBall GetOrCreateSpecialBall()
        {
            return _specialBallPool.Get();
        }

        /// <summary>
        /// Deactivates and returns balls to the pool.
        /// </summary>
        public void DestroyBalls(HashSet<Ball> balls)
        {
            foreach (var ball in balls)
            {
                ball.gameObject.SetActive(false);

                if (ball is SpecialBall specialBall)
                {
                    _specialBallPool.Release(specialBall);
                }
                else
                {
                    _regularBallPool.Release((RegularBall)ball);
                }
            }
        }
    }
}
