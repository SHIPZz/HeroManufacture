using CodeBase.Common.Services.Inputs;
using CodeBase.Common.Services.Raycast;
using CodeBase.Gameplay.Heroes.Configs;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Heroes.ActionComponents
{
    public class SetHeroMovementDestinationOnClick : ITickable
    {
        private readonly IInputService _inputService;
        private readonly HeroMovement _heroMovement;
        private readonly IRaycastService _raycastService;
        private readonly HeroConfig _heroConfig;

        public SetHeroMovementDestinationOnClick(
            IInputService inputService,
            HeroMovement heroMovement,
            HeroConfig heroConfig,
            IRaycastService raycastService)
        {
            _heroConfig = heroConfig;
            _inputService = inputService;
            _heroMovement = heroMovement;
            _raycastService = raycastService;
        }

        public void Tick()
        {
            if (!_inputService.GetLeftMouseButtonDown())
                return;
            
            if (TryGetClickPosition(out Vector3 position)) 
                _heroMovement.SetDestination(position);
        }

        private bool TryGetClickPosition(out Vector3 position)
        {
            position = Vector3.zero;

            bool hasWalkablePosition = _raycastService.TryGetWalkablePosition(_inputService.GetScreenMousePosition(), out position, _heroConfig.Mask);
            
            if (!hasWalkablePosition)
            {
                Debug.Log($"[HeroClickMovementHandler] Click detected but no walkable position found. Mask: {_heroConfig.Mask.value}");
            }

            return hasWalkablePosition;
        }
    }
}