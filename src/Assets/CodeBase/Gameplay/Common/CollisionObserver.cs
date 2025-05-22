using UnityEngine;
using UniRx;
using System;
using System.Collections.Generic;

namespace CodeBase.Gameplay.Common
{
    public class CollisionObserver : MonoBehaviour
    {
        private readonly ReactiveCollection<Collider> _currentColliders = new();
        private readonly Subject<Collider> _onEnter = new();
        private readonly Subject<Collider> _onExit = new();

        public IReadOnlyReactiveCollection<Collider> CurrentColliders => _currentColliders;
        public IObservable<Collider> OnEnter => _onEnter;
        public IObservable<Collider> OnExit => _onExit;

        private void OnTriggerEnter(Collider other)
        {
            if (!_currentColliders.Contains(other))
            {
                _currentColliders.Add(other);
                _onEnter.OnNext(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_currentColliders.Contains(other))
            {
                _currentColliders.Remove(other);
                _onExit.OnNext(other);
            }
        }

        private void OnDestroy()
        {
            _currentColliders?.Dispose();
            _onEnter?.Dispose();
            _onExit?.Dispose();
        }
    }
} 