using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    public abstract class Ball : MonoBehaviour
    {
        public BallConfigSO BallType { get; private set; }
        private bool _isMatched = false;

        public void SetType(BallConfigSO data)
        {
            BallType = data;
            GetComponent<SpriteRenderer>().sprite = data.Sprite;
        }

        public void MarkAsMatched()
        {
            if (_isMatched) return;
            _isMatched = true;
            BallPool.Instance.DestroyBalls(new List<Ball> { this });
        }
        public abstract void ExploseBalls();

    }
}
