using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;


    void Start()
    {
        ScoreManager.OnScoreUpdate += OnScoreUpdateHandle;
    }

    void OnScoreUpdateHandle(int score)
    {
        _scoreText.text = $"{score}";
    }
    private void OnDestroy()
    {
        ScoreManager.OnScoreUpdate -= OnScoreUpdateHandle;
    }
}
