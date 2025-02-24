using System;

/// <summary>
/// The ScoreManager will manager the score of each game.
/// </summary>
public class ScoreManager : Singleton<ScoreManager>
{
    public int Score { get; private set; } = 0;
    int _targetScore = 400;

    public static Action<int> OnScoreUpdate;
    const string SCORE_KEY = "ScoreKey";

    public void Initialize(int targetScore)
    {
        base.Initialize();
        _targetScore = targetScore;
        OnScoreUpdate?.Invoke(0);
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
    public int GetHighScore()
    {
        return Utils.GetData<int>(SCORE_KEY);
    }
    public int GetTargetScore()
    {
        return _targetScore;
    }
    public bool HaveWin()
    {
        return Score > _targetScore;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameEvents.OnGameStart -= OnGameStart;
        GameEvents.OnGameEnd -= SaveScore;
    }
}