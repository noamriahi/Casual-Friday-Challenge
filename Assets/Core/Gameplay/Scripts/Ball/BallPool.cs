using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    public class BallPool : MonoBehaviour
    {
        [Header("Spawn Objects")]
        [SerializeField] Ball _ballPrefab;
        [SerializeField] Transform _ballParent;
        [SerializeField] Transform _spawnPoint;
        [Header("Spawn Configs")]
        [SerializeField] int _ballCount = 10;
        [SerializeField] float _spawnDelay = .01f;

        List<Ball> _ballsList = new();

        public static BallPool Instance;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            _ballsList = new List<Ball>(_ballCount);
            StartCoroutine(CreateBallsCoroutine());
        }

        private IEnumerator CreateBallsCoroutine()
        {
            for (int i = 0; i < _ballCount; i++)
            {
                Ball newBall = Instantiate(_ballPrefab, _spawnPoint.position, Quaternion.identity, _ballParent);
                _ballsList.Add(newBall);
                yield return new WaitForSeconds(_spawnDelay);
                
                if (i % 3 == 0)
                {
                    newBall.GetComponent<SpriteRenderer>().material.color = Color.red;
                    newBall.BallType = "Red";
                }
                else
                {
                    newBall.GetComponent<SpriteRenderer>().material.color = Color.blue;
                    newBall.BallType = "Blue";
                }
                BallManager.Instance.RegisterBall(newBall);

            }
        }

        public void DestroyBalls(Ball[] list)
        {
            foreach (var ball in list)
            {
                ball.transform.position = _spawnPoint.position;
            }
        }
    }

}