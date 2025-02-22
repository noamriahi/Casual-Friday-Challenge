using Cysharp.Threading.Tasks;
using UnityEngine;

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
        TapManager.OnTapOverEvent += OnGameOver;
        await StartGame();
    }
    async UniTask StartGame()
    {
        _gamePlayUI.ToggleStartGamePopup(true);
        await UniTask.WaitForSeconds(2f);
        _gamePlayUI.ToggleStartGamePopup(false);

    }
    void OnGameOver()
    {
        ScoreManager.Instance.SaveScore();
        _gamePlayUI.ToggleEndGamePopup(true);

    }
    private void OnDestroy()
    {
    }
}
