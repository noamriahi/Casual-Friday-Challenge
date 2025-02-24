using Core.Popups;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class StartGamePopup : Popup
{
    [Header("Start Game Popup")]
    [SerializeField] TMP_Text _targetScoreText;
    [SerializeField] TMP_Text _highScoreText;

    [Header("Transition")]
    [SerializeField] RectTransform _uiFrame;
    public override void InitPopup()
    {
        _uiFrame.ShowFromBottom(0.3f);
        SetTexts();
        DelayBeforeDestroy();
    }
    private void SetTexts()
    {
        _targetScoreText.text = $"Target Score : {ScoreManager.Instance.GetTargetScore()}";
        _highScoreText.text = $"High Score: {ScoreManager.Instance.GetHighScore()}";
    }
    private async void DelayBeforeDestroy()
    {
        await UniTask.WaitForSeconds(1f);
        ClosePopup();
    }
    protected override void ClosePopup()
    {
        _uiFrame.DisapearToTop(0.3f, () =>
        {
            base.ClosePopup();
        });
    }
}
