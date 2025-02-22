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
        SaveScore();
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
}