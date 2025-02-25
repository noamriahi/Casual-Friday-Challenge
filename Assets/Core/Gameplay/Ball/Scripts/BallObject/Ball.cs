using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Balls
{
    public abstract class Ball : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _exploseEffect;
        [SerializeField] private SpriteRenderer _spriteRenderer;


        public BallConfigSO BallType { get; private set; }
        public static Action<List<Ball>, Vector3> OnDestroyBalls;
        public static Action OnPressNoEnoughBalls;

        public void Initialize(BallConfigSO data)
        {
            BallType = data;
            _spriteRenderer.sprite = data.Sprite;
            _spriteRenderer.enabled = true;
        }
        public void DestroyBall()
        {
            StartCoroutine(DestroyBallCoroutine());
        }
        IEnumerator DestroyBallCoroutine()
        {
            _spriteRenderer.enabled = false;
            _exploseEffect.Play();
            yield return new WaitForSeconds(0.5f);
            BallPool.Instance.DestroyBalls(new HashSet<Ball> { this });
        }
        public abstract void Explode();

    }
}
