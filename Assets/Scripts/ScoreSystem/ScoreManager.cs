using System;

/// <summary>
/// The ScoreManager will manager the score of each game.
/// </summary>
public class ScoreManager
{
    public int Score { get; private set; }
    private int _targetScore;
    public static Action<int> OnScoreUpdate;

    public ScoreManager(int targetScore)
    {
        _targetScore = targetScore;
    }

    public void AddPoints(int points)
    {
        Score += points;
        OnScoreUpdate?.Invoke(Score);
    }

    public bool HasReachedTarget()
    {
        return Score >= _targetScore;
    }
}