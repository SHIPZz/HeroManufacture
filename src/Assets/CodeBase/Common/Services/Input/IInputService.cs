using UnityEngine;

namespace CodeBase.Common.Services.Input
{
  public interface IInputService
  {
    float GetVerticalAxis();
    float GetHorizontalAxis();
    bool HasAxisInput();
    
    bool GetLeftMouseButtonDown();
    Vector2 GetScreenMousePosition();
    Vector2 GetWorldMousePosition();
    bool GetLeftMouseButtonUp();
    float GetMouseX();
    float GetMouseY();
    Camera CameraMain { get; }
    bool GetRightMouseButtonDown();
    bool GetRightMouseButtonUp();
    bool HasMouseAxis();
    Vector3 GetAxis();
  }
}