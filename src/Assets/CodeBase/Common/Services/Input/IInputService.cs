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
    bool IsAttacking();
    bool ReloadPressed { get; }
    Camera CameraMain { get; }
    bool IsRunningPressed();
    bool IsAiming();
    bool GetRightMouseButtonDown();
    bool GetRightMouseButtonUp();
    bool HasMouseAxis();
    Vector3 GetAxis();
    bool IsJumpButtonPressed();
    bool IsDoubleAttacking();
    bool IdleFocusPressed();
  }
}