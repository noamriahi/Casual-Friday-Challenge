using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] GameObject _startGamePopup;


    void Start()
    {
        ScoreManager.OnScoreUpdate += OnScoreUpdateHandle;
        _startGamePopup.SetActive(false);
    }
    public void ToggleStartGamePopup(bool state)
    {
        _startGamePopup.SetActive(state);
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
