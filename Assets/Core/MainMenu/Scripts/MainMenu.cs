using Core.Popups;
using Cysharp.Threading.Tasks;
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
        LoadGaneScene().Forget();
    }
    async UniTaskVoid LoadGaneScene()
    {
        await Fader.Instance.FadeIn();
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("Gameplay");
        await loadOperation.ToUniTask();
        await Fader.Instance.FadeOut();
    }
    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(StartGame);
        _settingsButton.onClick.RemoveListener(OpenSettings);
    }
}
