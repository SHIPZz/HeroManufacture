using CodeBase.Common.Services.Inputs;
using UnityEngine;
using UnityEngine.AI;
using CodeBase.UI.Services;
using UnityEngine.EventSystems;

namespace CodeBase.Common.Services.Raycast
{
    public class RaycastService : IRaycastService
    {
        private const float MaxDistance = 3f;
        
        private readonly IInputService _inputService;
        private readonly LayerMask _mask;

        public RaycastService(IInputService inputService)
        {
            _inputService = inputService;
        }

        public bool TryGetWalkablePosition(Vector2 screenPosition, out Vector3 position, LayerMask mask)
        {
            position = Vector3.zero;
            
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("[RaycastService] Clicked on UI, ignoring raycast.");
                return false;
            }
            
            if (_inputService.CameraMain == null)
            {
                Debug.LogError("[RaycastService] Main camera is null!");
                return false;
            }

            Ray ray = _inputService.CameraMain.ScreenPointToRay(screenPosition);
            Debug.Log($"[RaycastService] Casting ray from screen position: {screenPosition}");

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask))
                return false;

            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, MaxDistance, NavMesh.AllAreas))
            {
                Debug.Log($"[RaycastService] Found walkable position near: {hit.point} at: {navHit.position}");
                position = navHit.position;
                return true;
            }

            Debug.Log($"[RaycastService] No walkable position found near: {hit.point} within {MaxDistance} units");
            return false;
        }
    }
    
} 