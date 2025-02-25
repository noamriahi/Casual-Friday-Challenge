using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Gameplay/Game Config")]
public class GameConfigSO : ScriptableObject
{
    public int gameTime;
    public int targetScore;
    public int maxTap;
}
