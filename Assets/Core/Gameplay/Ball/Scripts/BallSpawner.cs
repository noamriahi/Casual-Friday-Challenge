using Core.Balls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] Transform _ballSpawnPoint;
    [SerializeField] float _spawnDelay = 0.05f;
    public void SpawnBalls(int ballAmount, List<Ball> ballInBoard)
    {
        StartCoroutine(ProceedSpawnBalls(ballAmount, ballInBoard));
    }
    IEnumerator ProceedSpawnBalls(int ballAmount, List<Ball> ballInBoard)
    {
        for (int i = 0; i < ballAmount; i++)
        {
            var ball = BallPool.Instance.CreateRegularBall(_ballSpawnPoint.position);
            ballInBoard.Add(ball);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
