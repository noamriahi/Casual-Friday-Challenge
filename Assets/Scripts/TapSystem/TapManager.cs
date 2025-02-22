using System;
using UnityEngine;

public class TapManager : Singleton<TapManager>
{
    [SerializeField] int _maxTapAmount = 20;

    public static Action<int> OnTapUpdateEvent;
    int _currentTapAmount = 0;
    

    public override void Initialize()
    {
        base.Initialize();
        GameEvents.OnGameStart += OnGameStart;
    }
    void OnGameStart()
    {
        _currentTapAmount = _maxTapAmount;
        OnTapUpdateEvent?.Invoke(Instance._currentTapAmount);
    }
    public void TapBall()
    {
        _currentTapAmount--;
        OnTapUpdateEvent?.Invoke(_currentTapAmount);
    }
    public bool IsGameOver()
    {
        return _currentTapAmount <= 0;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameEvents.OnGameStart -= OnGameStart;

    }
}
