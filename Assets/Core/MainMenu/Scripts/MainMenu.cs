using Core.Popups;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button _playButton;
    [SerializeField] TMP_Text _highScoreText;
    [SerializeField] Button _settingsButton;
    void Start()
    {
        int highScore = Utils.GetData<int>(ScoreManager.SCORE_KEY);
        _highScoreText.text = $"{highScore}";
        _playButton.onClick.AddListener(StartGame);
        _settingsButton.onClick.AddListener(OpenSettings);
    }
    void OpenSettings()
    {
        new OpenPopupCommand("Popups/SettingsPopup").Execute();
    }
    void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(StartGame);
        _settingsButton.onClick.RemoveListener(OpenSettings);
    }
}
