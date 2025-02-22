using System;

public class TapManager : Singleton<TapManager>
{
    public static Action OnTapOverEvent;
    public static Action<int> OnTapUpdateEvent;

    public int TapAmount = 5;

    public override void Initialize()
    {
        base.Initialize();
        OnTapUpdateEvent?.Invoke(Instance.TapAmount);
    }
    public void TapBall()
    {
        TapAmount--;
        OnTapUpdateEvent?.Invoke(TapAmount);
        if (TapAmount <= 0)
        {
            OnTapOverEvent?.Invoke();
        }
    }
}
