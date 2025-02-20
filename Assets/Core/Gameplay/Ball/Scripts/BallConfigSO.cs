using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBallConfig", menuName = "Gameplay/Ball Config")]
public class BallConfigSO : ScriptableObject
{
    public string TypeName; // TODO: Change to enum
    public Sprite Sprite;
}
