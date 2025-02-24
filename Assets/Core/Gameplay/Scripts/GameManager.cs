using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The GameManager will control the game state
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] GameplayUI _gamePlayUI;
    [SerializeField] GameConfigSO _gameConfig;
    void Start()
    {
        ScoreManager.Instance.Initialize(_gameConfig.targetScore);
        TapManager.Instance.Initialize(_gameConfig.maxTap);
        _gamePlayUI.Initialize(_gameConfig.gameTime);
        GameEvents.OnGameEnd += OnGameOver;
        StartGame();
    }
    async void StartGame()
    {
        _gamePlayUI.ShowStartGamePopup(true);
        await UniTask.WaitForSeconds(2f);
        _gamePlayUI.ShowStartGamePopup(false);

        GameEvents.OnGameStart?.Invoke();
    }
    void OnGameOver()
    {
        _gamePlayUI.ShowEndGamePopup();

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
