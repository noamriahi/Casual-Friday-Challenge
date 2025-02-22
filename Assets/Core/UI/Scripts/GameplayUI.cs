using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [Header("Top UI Texts")]
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _tapText;
    [SerializeField] TMP_Text _timerText;
    [Header("Game Manager")]
    [SerializeField] GameObject _startGamePopup;
    [SerializeField] GameObject _endGamePopup;


    void Awake()
    {
        ScoreManager.OnScoreUpdate += OnScoreUpdateHandle;
        TapManager.OnTapUpdateEvent += OnTapUpdateHandle;
        _startGamePopup.SetActive(false);
    }
    public void ToggleStartGamePopup(bool state)
    {
        _startGamePopup.SetActive(state);
    }
    public void ToggleEndGamePopup(bool state)
    {
        _endGamePopup.SetActive(state);
    }
    void OnScoreUpdateHandle(int score)
    {
        _scoreText.text = $"{score}";
    }
    void OnTapUpdateHandle(int tapAmount)
    {
        _tapText.text = $"{tapAmount}";
    }
    private void OnDestroy()
    {
        ScoreManager.OnScoreUpdate -= OnScoreUpdateHandle;
        TapManager.OnTapUpdateEvent -= OnTapUpdateHandle;
    }
}
