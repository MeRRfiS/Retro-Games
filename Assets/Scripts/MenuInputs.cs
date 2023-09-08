using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuInputs : MonoBehaviour
{
    public void StartGame(InputAction.CallbackContext callback)
    {
        if (!callback.started) return;

        SceneManager.LoadScene(SceneConstants.RETGO_GAME);
    }

    public void CloseGame(InputAction.CallbackContext callback)
    {
        if (!callback.started) return;

        Application.Quit();
    }
}
