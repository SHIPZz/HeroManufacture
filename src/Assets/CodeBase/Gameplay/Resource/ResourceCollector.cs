using System;
using System.Collections.Generic;
using System.Threading;
using CodeBase.Gameplay.Common;
using CodeBase.Gameplay.Items;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Gameplay.Resource
{
    public class ResourceCollector : MonoBehaviour
    {
        [SerializeField] private CollisionObserver _resourceGeneratorCollisionObserver;

        private readonly Subject<Dictionary<ItemTypeId, int>> _onResourcesCollected = new();
        private readonly HashSet<ResourceGenerator> _activeResourceGenerators = new();
        private CancellationTokenSource _cancellationToken;

        public IObservable<Dictionary<ItemTypeId, int>> OnResourcesCollected => _onResourcesCollected;

        private void Awake() => SubscribeToCollisions();

        private void SubscribeToCollisions()
        {
            _resourceGeneratorCollisionObserver.OnEnter
                .Where(collider => collider != null)
                .Subscribe(collider =>
                {
                    var generator = collider.GetComponent<ResourceGenerator>();
                    if (generator != null)
                    {
                        _activeResourceGenerators.Add(generator);
                        StartResourceCollection();
                    }
                })
                .AddTo(this);

            _resourceGeneratorCollisionObserver.OnExit
                .Where(collider => collider != null)
                .Subscribe(collider =>
                {
                    var generator = collider.GetComponent<ResourceGenerator>();
                    if (generator != null)
                    {
                        _activeResourceGenerators.Remove(generator);
                        if (_activeResourceGenerators.Count == 0)
                        {
                            StopResourceCollection();
                        }
                    }
                })
                .AddTo(this);
        }

        private void StartResourceCollection()
        {
            StopResourceCollection();

            if (_cancellationToken is null or { IsCancellationRequested: true })
                _cancellationToken = new CancellationTokenSource();

            CollectResourcesAsync(_cancellationToken.Token).Forget();
        }

        private async UniTask CollectResourcesAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested && _activeResourceGenerators.Count > 0)
            {
                foreach (var generator in _activeResourceGenerators)
                {
                    if (generator != null && generator.IsResourceReady)
                    {
                        Dictionary<ItemTypeId, int> collectedResources = generator.CollectResource();

                        if (collectedResources is { Count: > 0 })
                        {
                            SendResourceCollectedEvent(collectedResources);
                            DictionaryPool<ItemTypeId, int>.Release(collectedResources);
                        }
                    }
                }

                await UniTask.Yield(token);
            }
        }

        private void OnDestroy()
        {
            StopResourceCollection();
            _onResourcesCollected.Dispose();
            _activeResourceGenerators.Clear();
        }

        private void SendResourceCollectedEvent(Dictionary<ItemTypeId, int> collectedResources) => _onResourcesCollected?.OnNext(collectedResources);

        private void StopResourceCollection()
        {
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();
            _cancellationToken = null;
        }
    }
}