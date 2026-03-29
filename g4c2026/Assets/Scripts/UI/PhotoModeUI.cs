using UnityEngine;

public class PhotoModeUI : MonoBehaviour {
    public GameObject PhotoModeObj;

    void Update() {
        if (GameManager.I().CurrentGameState == GameState.Picture) PhotoModeObj.SetActive(true);
        else PhotoModeObj.SetActive(false);
    }
}
