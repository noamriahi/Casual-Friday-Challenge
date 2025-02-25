using Core.Popups;
using Core.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The GameManager will control the game state, The game config and the scene tranaition
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
    /// <summary>
    /// Use the settings to set the difficulty level of the game, the config are set using ScriptableObject
    /// </summary>
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
    /// <summary>
    /// Open the start popup using a command and addressable and start the game
    /// </summary>
    void StartGame()
    {
        new OpenPopupCommand(PopupType.StartGame).Execute();

        GameEvents.OnGameStart?.Invoke();
    }
    /// <summary>
    /// Open the end game popup using a command pattern and addressable
    /// </summary>
    void OnGameOver()
    {
        new OpenPopupCommand(PopupType.EndGame).Execute();

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
