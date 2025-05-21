using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Common.Services.Input
{
    public class StandaloneInputService : IInputService
    {
        private Camera _mainCamera;
        private Vector3 _screenPosition;

        public Camera CameraMain
        {
            get
            {
                if (_mainCamera == null && Camera.main != null)
                    _mainCamera = Camera.main;

                return _mainCamera;
            }
        }

        public Vector2 GetScreenMousePosition() =>
            CameraMain ? (Vector2)UnityEngine.Input.mousePosition : new Vector2();

        public Vector2 GetWorldMousePosition()
        {
            if (CameraMain == null)
                return Vector2.zero;

            _screenPosition.x = UnityEngine.Input.mousePosition.x;
            _screenPosition.y = UnityEngine.Input.mousePosition.y;
            return CameraMain.ScreenToWorldPoint(_screenPosition);
        }

        public bool HasAxisInput() => GetHorizontalAxis() != 0 || GetVerticalAxis() != 0;


        public float GetVerticalAxis() => UnityEngine.Input.GetAxis("Vertical");

        public float GetHorizontalAxis() => UnityEngine.Input.GetAxis("Horizontal");

        public Vector3 GetAxis() => new(GetHorizontalAxis(),0,GetVerticalAxis());

        public float GetMouseX() => UnityEngine.Input.GetAxisRaw("Mouse X");

        public float GetMouseY() => UnityEngine.Input.GetAxisRaw("Mouse Y");

        public bool HasMouseAxis() => GetMouseX() != 0 || GetMouseY() != 0;

        public bool GetRightMouseButtonDown() => UnityEngine.Input.GetMouseButtonDown(1);

        public bool GetRightMouseButtonUp() => UnityEngine.Input.GetMouseButtonUp(1);

        public bool GetLeftMouseButtonDown() =>
            UnityEngine.Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject();

        public bool GetLeftMouseButtonUp() =>
            UnityEngine.Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject();
    }
}