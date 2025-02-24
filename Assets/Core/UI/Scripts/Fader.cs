using Core;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Fader : UnitySingleton<Fader>
{
    [SerializeField] CanvasGroup _fader;

    protected override void Start()
    {
        base.Start();
        _fader.gameObject.SetActive(false);
    }
    public async UniTask FadeIn()
    {
        _fader.alpha = 0f;
        _fader.gameObject.SetActive(true); 
        Tweener tweener = _fader.DOFade(1f, .5f);
        await tweener.AsyncWaitForCompletion();
    }
    public async UniTask FadeOut()
    {
        _fader.alpha = 1f;
        _fader.gameObject.SetActive(true);
        Tweener tweener = _fader.DOFade(0f, .5f);
        await tweener.AsyncWaitForCompletion();

        _fader.gameObject.SetActive(false);

    }
}
