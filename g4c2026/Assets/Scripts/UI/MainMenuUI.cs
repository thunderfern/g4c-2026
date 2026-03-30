using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    public GameObject MainMenuObj;

    void Update() {
        if (GameManager.I().CurrentGameState != GameState.MainMenu) MainMenuObj.SetActive(false);
        else MainMenuObj.SetActive(true);
    }
}
