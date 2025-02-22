using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    public abstract class Ball : MonoBehaviour
    {
        [SerializeField] ParticleSystem _exploseEffect;
        [SerializeField] SpriteRenderer _spriteRenderer;

        public BallConfigSO BallType { get; private set; }
        public static Action<List<Ball>, Vector3> OnDestroyBalls;
        
        public void SetType(BallConfigSO data)
        {
            _spriteRenderer.enabled = true;
            BallType = data;
            _spriteRenderer.sprite = data.Sprite;
        }
        public void DestroyBall()
        {
            StartCoroutine(DestroyBallProcess());
        }
        IEnumerator DestroyBallProcess()
        {
            _spriteRenderer.enabled = false;
            _exploseEffect.Play();
            yield return new WaitForSeconds(0.5f);
            BallPool.Instance.DestroyBalls(this);

            _spriteRenderer.enabled = true;
        }
        public abstract void ExploseBalls();

    }
}
