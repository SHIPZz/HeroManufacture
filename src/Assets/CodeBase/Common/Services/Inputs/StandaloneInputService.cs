using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Common.Services.Inputs
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
           CameraMain ? Input.mousePosition : new Vector2();

        public Vector2 GetWorldMousePosition()
        {
            if (CameraMain == null)
                return Vector2.zero;

            _screenPosition.x = Input.mousePosition.x;
            _screenPosition.y = Input.mousePosition.y;
            return CameraMain.ScreenToWorldPoint(_screenPosition);
        }

        public bool HasAxisInput() =>  GetHorizontalAxis() != 0 || GetVerticalAxis() != 0;

        public float GetVerticalAxis() =>  Input.GetAxis("Vertical");

        public float GetHorizontalAxis() =>  Input.GetAxis("Horizontal");

        public Vector3 GetAxis() =>  new(GetHorizontalAxis(), 0, GetVerticalAxis());

        public float GetMouseX() =>  Input.GetAxisRaw("Mouse X");

        public float GetMouseY() =>  Input.GetAxisRaw("Mouse Y");

        public bool HasMouseAxis() => (GetMouseX() != 0 || GetMouseY() != 0);

        public bool GetRightMouseButtonDown() =>  Input.GetMouseButtonDown(1);

        public bool GetRightMouseButtonUp() =>  Input.GetMouseButtonUp(1);

        public bool GetLeftMouseButtonDown() =>
            Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject();

        public bool GetLeftMouseButtonUp() =>
             Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject();
    }
}