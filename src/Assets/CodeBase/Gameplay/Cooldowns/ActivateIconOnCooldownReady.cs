using System;
using System.Threading;
using CodeBase.Animations;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Gameplay.Cooldowns
{
    public class ActivateIconOnCooldownReady : MonoBehaviour
    {
        [SerializeField] private Cooldown _cooldown;
        [SerializeField] private ScaleAnimator _scaleAnimator;
        [SerializeField] private FloatAnimation _floatAnimation;
        [SerializeField] private float _waitTime = 5f;

        private CancellationTokenSource _cancellationTokenSource;

        private void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            
            _cooldown.IsReady
                .Subscribe(HandleCooldownReady)
                .AddTo(this);
        }

        private void HandleCooldownReady(bool ready)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();

            if (ready)
            {
                WaitAndAppear(_cancellationTokenSource.Token).Forget();
            }
            else
            {
                DisappearIcon();
            }
        }

        private async UniTaskVoid WaitAndAppear(CancellationToken token)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_waitTime), cancellationToken: token);
                AppearIcon();
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void DisappearIcon()
        {
            _floatAnimation.StopFloatAnimation();
            _scaleAnimator.AnimateToZero();
        }

        private void AppearIcon()
        {
            _scaleAnimator.AnimateToOne();
            _floatAnimation.StartFloatAnimation();
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}