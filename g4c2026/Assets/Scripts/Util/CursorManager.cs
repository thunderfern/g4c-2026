using UnityEngine;

public class CursorManager : MonoBehaviour {

    void Update() {
        if (GameManager.I().CurrentGameState == GameState.MainMenu || 
            GameManager.I().CurrentGameState == GameState.Photobook || 
            GameManager.I().CurrentGameState == GameState.Settings || 
            Input.GetKey(KeyCode.LeftControl)) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}