using UnityEngine;
using UniRx;
using System;

namespace CodeBase.Gameplay.Common
{
    public class CollisionObserver : MonoBehaviour
    {
        private readonly ReactiveProperty<Collider> _currentCollider = new ReactiveProperty<Collider>();
        private readonly Subject<Collider> _onEnter = new Subject<Collider>();
        private readonly Subject<Collider> _onExit = new Subject<Collider>();

        public IReadOnlyReactiveProperty<Collider> CurrentCollider => _currentCollider;
        public IObservable<Collider> OnEnter => _onEnter;
        public IObservable<Collider> OnExit => _onExit;

        private void OnTriggerEnter(Collider other)
        {
            _currentCollider.Value = other;
            _onEnter.OnNext(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (_currentCollider.Value == other)
            {
                _currentCollider.Value = null;
            }
            _onExit.OnNext(other);
        }

        private void OnDestroy()
        {
            _currentCollider?.Dispose();
            _onEnter?.Dispose();
            _onExit?.Dispose();
        }
    }
} 