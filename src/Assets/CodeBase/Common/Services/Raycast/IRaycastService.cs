using UnityEngine;

namespace CodeBase.Common.Services.Raycast
{
    public interface IRaycastService
    {
        bool TryGetWalkablePosition(Vector2 screenPosition, out Vector3 position, LayerMask mask);
    }
}