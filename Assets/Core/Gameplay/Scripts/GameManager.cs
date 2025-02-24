using Core.Popups;
using Core.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The GameManager will control the game state
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] GameplayUI _gameplayUI;
    [SerializeField] GameConfigSO _gameConfig;
    private void Start()
    {
        InitializeGame();
    }
    private void InitializeGame()
    {
        ScoreManager.Instance.Initialize(_gameConfig.targetScore);
        TapManager.Instance.Initialize(_gameConfig.maxTap);
        _gameplayUI.Initialize(_gameConfig.gameTime);

        GameEvents.OnGameEnd += OnGameOver;
        StartGame();
    }

    void StartGame()
    {
        new OpenPopupCommand("Popups/StartGamePopup").Execute();

        GameEvents.OnGameStart?.Invoke();
    }
    void OnGameOver()
    {
        new OpenPopupCommand("Popups/EndGamePopup").Execute();

    }
    private void OnDestroy()
    {
        GameEvents.OnGameEnd -= OnGameOver;
    }
    public static void ReturnToLobby()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public static void RestartGame()
    {
        GameEvents.OnGameStart?.Invoke();
    }
}
