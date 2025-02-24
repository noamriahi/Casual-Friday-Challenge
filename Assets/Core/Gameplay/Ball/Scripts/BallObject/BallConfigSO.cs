using Core.Balls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBallConfig", menuName = "Gameplay/Ball Config")]
public class BallConfigSO : ScriptableObject
{
    public BallType TypeName;
    public Sprite Sprite;
}
