using Cysharp.Threading.Tasks;
using System;
using System.Threading;
public class Timer
{
    private float timeRemaining;
    private CancellationTokenSource cts;

    public Action<float> OnTimerUpdate;
    public Action OnTimerEnd;

    public Timer(int totalTime)
    {
        timeRemaining = totalTime;

        cts = new CancellationTokenSource();
        StartCountdown(cts.Token).Forget(); 
    }
    public Timer WithTickCallback(Action<float> callback)
    {
        OnTimerUpdate = callback;
        return this;
    }

    public Timer WithEndTimerCallback(Action callback)
    {
        OnTimerEnd = callback;
        return this;    
    }
    private async UniTaskVoid StartCountdown(CancellationToken token)
    {
        while (timeRemaining > 0)
        {
            await UniTask.Delay(1000, cancellationToken: token);
            if (token.IsCancellationRequested) return; 

            timeRemaining -= 1;
            UpdateTimerDisplay();
        }

        GameOver();
    }

    private void UpdateTimerDisplay()
    {
        OnTimerUpdate?.Invoke(timeRemaining);
    }

    private void GameOver()
    {
        OnTimerEnd?.Invoke();
        OnTimerEnd = null;
        OnTimerUpdate = null;
    }

    public void CancelTimer()
    {
        if (cts != null)
        {
            cts.Cancel(); // Stop the countdown
        }
        OnTimerEnd = null;
        OnTimerUpdate = null;
    }
}
