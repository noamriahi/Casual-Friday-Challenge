using Core.Popups;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class StartGamePopup : Popup
{
    [SerializeField] TMP_Text _targetScoreText;
    [SerializeField] TMP_Text _highScoreText;
    public override void InitPopup()
    {
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
        await UniTask.WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
