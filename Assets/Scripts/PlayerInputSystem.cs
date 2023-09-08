using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    public void Moving(InputAction.CallbackContext context)
    {
        PlayerController.GetInstance().ChangeMovement(context.ReadValue<Vector2>());

        if (context.canceled)
        {
            PlayerController.GetInstance().IsFreeMoving = true;
        }
    }
}
