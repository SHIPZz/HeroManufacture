using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Common.Services.Inputs
{
    public class MobileInputService : IInputService
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

        public Vector2 GetScreenMousePosition()
        {
            return Input.touchCount <= 0 ? Vector2.zero : Input.GetTouch(0).position;
        }

        public Vector2 GetWorldMousePosition()
        {
            if (CameraMain == null)
                return Vector2.zero;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                _screenPosition.x = touch.position.x;
                _screenPosition.y = touch.position.y;
                return CameraMain.ScreenToWorldPoint(_screenPosition);
            }

            return Vector2.zero;
        }

        public bool HasAxisInput() => GetHorizontalAxis() != 0 || GetVerticalAxis() != 0;

        public float GetVerticalAxis() => SimpleInput.GetAxis("Vertical");

        public float GetHorizontalAxis() => SimpleInput.GetAxis("Horizontal");

        public Vector3 GetAxis()
        {
            if (Input.touchCount == 0)
                return new Vector3();
            
            return new Vector3(GetHorizontalAxis(), 0, GetVerticalAxis());
        }

        public float GetMouseX() => SimpleInput.GetAxisRaw("Mouse X");

        public float GetMouseY() => SimpleInput.GetAxisRaw("Mouse Y");

        public bool HasMouseAxis() => GetMouseX() != 0 || GetMouseY() != 0;

        public bool GetRightMouseButtonDown() => Input.touchCount > 1 && Input.GetTouch(1).phase == TouchPhase.Began;

        public bool GetRightMouseButtonUp() => Input.touchCount > 1 && Input.GetTouch(1).phase == TouchPhase.Ended;

        public bool GetLeftMouseButtonDown()
        {
            if (Input.touchCount == 0)
                return false;

            if (Input.touches[0].phase == TouchPhase.Ended && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return true;

            return false;
        }

        public bool GetLeftMouseButtonUp() =>
            Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
    }
}