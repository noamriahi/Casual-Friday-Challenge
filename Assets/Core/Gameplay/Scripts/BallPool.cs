using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BallPool : MonoBehaviour
{
    [Header("Spawn Objects")]
    [SerializeField] GameObject _ballPrefab;
    [SerializeField] Transform _ballParent;
    [SerializeField] Transform _spawnPoint;
    [Header("Spawn Configs")]
    [SerializeField] int _ballCount = 10;
    [SerializeField] float _spawnDelay = .01f;

    List<GameObject> _ballsList = new();

    private void Start()
    {
        _ballsList = new List<GameObject>(_ballCount);
        StartCoroutine(CreateBallsCoroutine());
    }

    private IEnumerator CreateBallsCoroutine()
    {
        for (int i = 0; i < _ballCount; i++)
        {
            GameObject newBall = Instantiate(_ballPrefab, _spawnPoint.position, Quaternion.identity, _ballParent);
            _ballsList.Add(newBall);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    public GameObject GetBall()
    {
        foreach (var ball in _ballsList)
        {
            if (!ball.activeInHierarchy)
            {
                ball.SetActive(true);
                ball.transform.position = _spawnPoint.position;
                return ball;
            }
        }
        return null; // No available balls
    }

    public void DestroyBalls(GameObject[] list)
    {
        foreach (var ball in list)
        {
            ball.SetActive(false); // Disable instead of just repositioning
        }
    }
}
