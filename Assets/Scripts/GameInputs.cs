using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameInputs : MonoBehaviour
{
    public void ExitToMenu(InputAction.CallbackContext callback)
    {
        if (!CarGameController.GetInstance().GameIsOver) return;
        if (!callback.started) return;

        SceneManager.LoadScene(SceneConstants.MENU);
    }
}
