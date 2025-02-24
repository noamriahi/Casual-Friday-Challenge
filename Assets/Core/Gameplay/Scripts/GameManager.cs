using Core.Popups;
using Core.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The GameManager will control the game state
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] GameplayUI _gameplayUI;

    [Header("Game Config")]

    [SerializeField] GameConfigSO _easyConfig;
    [SerializeField] GameConfigSO _normalConfig;
    [SerializeField] GameConfigSO _hardConfig;

    GameConfigSO _gameConfig;
    private void Start()
    {
        InitializeGame();
    }
    private void InitializeGame()
    {
        SetConfig();
        ScoreManager.Instance.Initialize(_gameConfig.targetScore);
        TapManager.Instance.Initialize(_gameConfig.maxTap);
        _gameplayUI.Initialize(_gameConfig.gameTime);

        GameEvents.OnGameEnd += OnGameOver;
        StartGame();
    }
    void SetConfig()
    {
        var settings = Utils.GetData<SettingsData>(SettingsPopup.SETTINGS_DATA_KEY);
        if (settings == null)
            _gameConfig = _easyConfig;
        else
        {
            switch (settings.Difficulty)
            {
                case 0:
                    _gameConfig = _easyConfig;
                    break;
                case 1:
                    _gameConfig = _normalConfig;
                    break;
                case 2:
                    _gameConfig = _hardConfig;
                    break;
            }
        }
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
        LoadMenuScene().Forget();
    }
    static async UniTaskVoid LoadMenuScene()
    {
        await Fader.Instance.FadeIn();
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("MainMenu");
        await loadOperation.ToUniTask();
        await Fader.Instance.FadeOut();
    }
    public static void RestartGame()
    {
        GameEvents.OnGameStart?.Invoke();
    }
}
