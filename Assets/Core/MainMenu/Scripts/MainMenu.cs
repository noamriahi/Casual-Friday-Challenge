using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button _playButton;
    void Start()
    {
        _playButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(StartGame);
    }
}
