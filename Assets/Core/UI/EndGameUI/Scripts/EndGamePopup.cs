using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePopup : MonoBehaviour
{
    [SerializeField] Button _restartButton;
    [SerializeField] Button _returnButton;
    [SerializeField] TMP_Text _winText;
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _targetScoreText;
    [SerializeField] TMP_Text _highScoreText;
    void Start()
    {
        _restartButton.onClick.AddListener(RestartGame);
        _returnButton.onClick.AddListener(ReturnToLobby);
        bool haveWin = ScoreManager.Instance.HaveWin();
        _winText.text = haveWin ? "Win" : "Lose";
        _targetScoreText.text = $"/{ScoreManager.Instance.GetTargetScore()}";
        _scoreText.text = ScoreManager.Instance.Score.ToString();
        _scoreText.color = haveWin ? Color.green : Color.red;
        _highScoreText.text = $"High Score: {ScoreManager.Instance.GetHighScore()}";


    }
    void RestartGame()
    {
        GameManager.RestartGame();
        gameObject.SetActive(false);    
    }
    void ReturnToLobby()
    {
        GameManager.ReturnToLobby();
        gameObject.SetActive(false);
    }
}
