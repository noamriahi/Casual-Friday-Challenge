using System;

public class TapManager : Singleton<TapManager>
{
    int _maxTapAmount = 20;

    public static Action<int> OnTapUpdateEvent;
    int _currentTapAmount = 0;
    

    public void Initialize(int maxTap)
    {
        base.Initialize();
        _maxTapAmount = maxTap;
        OnTapUpdateEvent?.Invoke(_maxTapAmount);
        GameEvents.OnGameStart += OnGameStart;
    }
    void OnGameStart()
    {
        _currentTapAmount = _maxTapAmount;
        OnTapUpdateEvent?.Invoke(_currentTapAmount);
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
