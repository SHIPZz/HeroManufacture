using CodeBase.Common.Services.Inputs;
using UnityEngine;
using UnityEngine.AI;
using CodeBase.UI.Services;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
                return false;

            if (screenPosition == Vector2.zero)
                return false;

            Ray ray = _inputService.CameraMain.ScreenPointToRay(screenPosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask))
                return false;

            Debug.Log($"[RaycastService] Casting hit: {hit.collider.name}");

            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, MaxDistance, NavMesh.AllAreas))
            {
                position = navHit.position;
                return true;
            }

            return false;
        }
    }
}