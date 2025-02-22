using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The GameManager will control the game state
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] GameplayUI _gamePlayUI;
    async void Start()
    {
        ScoreManager.Instance.Initialize();
        TapManager.Instance.Initialize();
        GameEvents.OnGameEnd += OnGameOver;
        await StartGame();
    }
    async UniTask StartGame()
    {
        _gamePlayUI.ToggleStartGamePopup(true);
        await UniTask.WaitForSeconds(2f);
        _gamePlayUI.ToggleStartGamePopup(false);

        GameEvents.OnGameStart.Invoke();
    }
    void OnGameOver()
    {
        _gamePlayUI.ToggleEndGamePopup(true);

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
