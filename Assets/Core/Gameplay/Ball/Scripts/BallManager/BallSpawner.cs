using Core.Balls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField] Transform _ballSpawnPoint;
        [SerializeField] float _spawnDelay = 0.05f;
        public void SpawnBalls(int ballAmount)
        {
            StartCoroutine(ProceedSpawnBalls(ballAmount));
        }
        public void SpawnSpecialBall(Vector3 spawnPosition)
        {
            var specialBall = BallPool.Instance.SpawnSpecialBall(spawnPosition);
            BallManager.Instance.RegisterBall(specialBall);
        }
        IEnumerator ProceedSpawnBalls(int ballAmount)
        {
            for (int i = 0; i < ballAmount; i++)
            {
                var newBall = BallPool.Instance.CreateRegularBall(_ballSpawnPoint.position);
                BallManager.Instance.RegisterBall(newBall);
                yield return new WaitForSeconds(_spawnDelay);
            }
        }
    }
}
