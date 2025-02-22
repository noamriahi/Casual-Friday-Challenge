using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The GameManager will control the game state
/// </summary>
public class GameManager : MonoBehaviour
{
    private void Start()
    {
        ScoreManager.OnScoreUpdate += OnScoreUpdateHandler;
    }
    void OnScoreUpdateHandler(int score)
    {

    }
}
