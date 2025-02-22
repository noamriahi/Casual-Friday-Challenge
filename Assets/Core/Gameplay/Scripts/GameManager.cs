using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The GameManager will control the game state
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] GameplayUI _gamePlayUI;
    private void Start()
    {
        ScoreManager.Instance.Initialize();
        TapManager.Instance.Initialize();
        StartGame();
    }
    void StartGame()
    {
        _gamePlayUI.ToggleStartGamePopup(true);
    }
    private void OnDestroy()
    {
    }
}
