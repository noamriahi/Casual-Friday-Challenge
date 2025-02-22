using System;

/// <summary>
/// The ScoreManager will manager the score of each game.
/// </summary>
public class ScoreManager : Singleton<ScoreManager>
{
    public int Score { get; private set; } = 0;

    public static Action<int> OnScoreUpdate;
    const string SCORE_KEY = "ScoreKey";

    public override void Initialize()
    {
        base.Initialize();
        GameEvents.OnGameStart += OnGameStart;
        GameEvents.OnGameEnd += SaveScore;
    }
    void OnGameStart()
    {
        Score = 0;
        OnScoreUpdate?.Invoke(Score);
    }
    public void AddPoints(int points)
    {
        Score += points;
        OnScoreUpdate?.Invoke(Score);
    }
    public void SaveScore()
    {
        var oldScore = Utils.GetData<int>(SCORE_KEY);
        if(oldScore < Score)
        {
            Score.SaveData(SCORE_KEY);
        }
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameEvents.OnGameStart -= OnGameStart;
        GameEvents.OnGameEnd -= SaveScore;
    }
}