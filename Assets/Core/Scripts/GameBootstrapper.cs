using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
   // [SerializeField] private TimeManager _timeManager; // Stays MonoBehaviour

    private void Awake()
    {
        ScoreManager scoreManager = new ScoreManager(400); // Non-MonoBehaviour instance
        //_gameManager.Initialize(scoreManager);
    }
}