using System;
using System.Collections.Generic;
using System.Threading;
using CodeBase.Gameplay.Common;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Gameplay.Resource
{
    public class ResourceCollector : MonoBehaviour
    {
        [SerializeField] private CollisionObserver _resourceGeneratorCollisionObserver;

        private ResourceGenerator _currentResourceGenerator;
        private CancellationTokenSource _cancellationToken;

        private void Awake() => SubscribeToCollisions();

        private void SubscribeToCollisions()
        {
            _resourceGeneratorCollisionObserver.OnEnter
                .Where(collider => collider != null)
                .Subscribe(collider =>
                {
                    _currentResourceGenerator = collider.GetComponent<ResourceGenerator>();
                    StartResourceCollection();
                })
                .AddTo(this);

            _resourceGeneratorCollisionObserver.OnExit
                .Where(collider => collider != null)
                .Subscribe(_ =>
                {
                    StopResourceCollection();
                    _currentResourceGenerator = null;
                })
                .AddTo(this);
        }

        private void StartResourceCollection()
        {
            StopResourceCollection();

            if (_cancellationToken is null or { IsCancellationRequested: true })
                _cancellationToken = new CancellationTokenSource();

            try
            {
                CollectResourcesAsync(_cancellationToken.Token).Forget();
            }
            catch (Exception e) { }
        }

        private async UniTask CollectResourcesAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested && _currentResourceGenerator != null)
            {
                if (_currentResourceGenerator.IsResourceReady)
                {
                    Dictionary<ResourceType, int> collectedResources = _currentResourceGenerator.CollectResource();
                
                    if (collectedResources != null && collectedResources.Count > 0)
                    {
                        try
                        {
                            foreach (KeyValuePair<ResourceType, int> resource in collectedResources)
                            {
                                Debug.Log($"Collected {resource.Value} {resource.Key}");
                            }
                        }
                        finally
                        {
                            DictionaryPool<ResourceType, int>.Release(collectedResources);
                        }
                    }
                }

                await UniTask.Yield(token);
            }
        }

        private void OnDestroy()
        {
            StopResourceCollection();
        }

        private void StopResourceCollection()
        {
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();
            _cancellationToken = null;
        }
    }
}