using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    public abstract class Ball : MonoBehaviour
    {
        [SerializeField] ParticleSystem _exploseEffect;
        [SerializeField] SpriteRenderer _spriteRenderer;
        public BallConfigSO BallType { get; private set; }
        private bool _isMatched = false;
        public void SetType(BallConfigSO data)
        {
            _spriteRenderer.enabled = true;

            BallType = data;
            _spriteRenderer.sprite = data.Sprite;
        }

        public async UniTask MarkAsMatched()
        {
            if (_isMatched) return;
            _isMatched = true;
            await DestroyBall();
            //BallPool.Instance.DestroyBalls(new List<Ball> { this });
        }
        async UniTask DestroyBall()
        {
            _spriteRenderer.enabled = false;
            _exploseEffect.Play();
            await UniTask.WaitForSeconds(1f);
        }
        public abstract void ExploseBalls();

    }
}
