using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static partial class Utils
{
    public static void ShowFromBottom(this RectTransform uiObject, float duration, Action callback = null)
    {
        var originalPosition = uiObject.anchoredPosition;

        float offScreenY = -Screen.height;

        uiObject.anchoredPosition = new Vector2(originalPosition.x, offScreenY);

        // Animate to the saved target position.
        uiObject.DOAnchorPos(originalPosition, duration).OnComplete(()=>callback?.Invoke());
    }
    public static void DisapearToTop(this RectTransform uiObject, float duration, Action callback = null)
    {
        var originalPosition = uiObject.anchoredPosition;

        float offScreenY = Screen.height;

        var targetPosition = new Vector2(originalPosition.x, offScreenY);

        // Animate to the saved target position.
        uiObject.DOAnchorPos(targetPosition, duration).OnComplete(() => callback?.Invoke());
    }
}