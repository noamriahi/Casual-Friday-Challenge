using Core.Popups;
using Cysharp.Threading.Tasks;

public class StartGamePopup : Popup
{
    public override void InitPopup()
    {
        DelayBeforeDestroy();
    }
    private async void DelayBeforeDestroy()
    {
        await UniTask.WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
