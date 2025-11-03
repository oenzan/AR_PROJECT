using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public static class InputUtils
{
    /// <summary> Ekrana dokunma / mouse sol click yakalar. </summary>
    public static bool TryGetScreenTap(out Vector2 pos, bool ignoreUI = true)
    {
        pos = default;

        // UI üstüne tıklamayı yok say
        if (ignoreUI && EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return false;

        // New Input System - Touch
        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;
            if (touch.press.wasPressedThisFrame)
            {
                pos = touch.position.ReadValue();
                return true;
            }
        }

        // Mouse (Editor/PC)
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            pos = Mouse.current.position.ReadValue();
            return true;
        }

        return false;
    }
}
