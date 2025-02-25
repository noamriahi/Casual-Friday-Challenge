using DG.Tweening;
using System;
using UnityEngine;

/// <summary>
/// Part of the utils, There is function that make animation generic and easy.
/// I use it using dotween because as you know, Animator in UI is a bad practice.
/// </summary>
public static partial class Utils
{
    public static void ShowFromBottom(this RectTransform uiObject, float duration, Action callback = null)
    {
        var originalPosition = uiObject.anchoredPosition;

        float offScreenY = -Screen.height;

        uiObject.anchoredPosition = new Vector2(originalPosition.x, offScreenY);

        uiObject.DOAnchorPos(originalPosition, duration).OnComplete(()=>callback?.Invoke());
    }
    public static void DisapearToTop(this RectTransform uiObject, float duration, Action callback = null)
    {
        var originalPosition = uiObject.anchoredPosition;

        float offScreenY = Screen.height;

        var targetPosition = new Vector2(originalPosition.x, offScreenY);

        uiObject.DOAnchorPos(targetPosition, duration).OnComplete(() => callback?.Invoke());
    }
    public static void BounceScaleUI(this RectTransform uiObject, float duration, float scale = 1.3f, Action callback = null)
    {
        var scaleVector = new Vector2(scale, scale);
        uiObject.DOScale(scaleVector, duration / 2).OnComplete(() =>
        {
            uiObject.DOScale(Vector2.one, duration / 2).OnComplete(() => callback?.Invoke());
        });
    }
}