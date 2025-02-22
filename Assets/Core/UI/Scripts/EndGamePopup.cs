using UnityEngine;
using UnityEngine.UI;

public class EndGamePopup : MonoBehaviour
{
    [SerializeField] Button _restartButton;
    [SerializeField] Button _returnButton;
    void Start()
    {
        _restartButton.onClick.AddListener(RestartGame);
        _returnButton.onClick.AddListener(ReturnToLobby);
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
