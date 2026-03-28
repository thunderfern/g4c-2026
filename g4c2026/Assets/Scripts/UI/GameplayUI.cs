using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour {

    public GameObject GameplayObj;
    public Button PhotobookIcon;

    void Start() {
        PhotobookIcon.onClick.AddListener(() => {
            GameManager.I().CurrentGameState = GameState.Photobook;
        });
    }

    void Update() {
        if (GameManager.I().CurrentGameState == GameState.Movement) GameplayObj.SetActive(true);
        else GameplayObj.SetActive(false);
    }
}