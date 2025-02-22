using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePopup : MonoBehaviour
{
    [SerializeField] Button _restartButton;
    [SerializeField] Button _returnButton;
    [SerializeField] TMP_Text _winText;
    void Start()
    {
        _restartButton.onClick.AddListener(RestartGame);
        _returnButton.onClick.AddListener(ReturnToLobby);
        _winText.text = ScoreManager.Instance.HaveWin() ? "Win" : "Lose";
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
