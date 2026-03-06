using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    // Singleton
    private static GameManager _instance;

    private GameManager() {
        _instance = this;
    }

    public static GameManager I() {
        if (_instance == null) {
            GameManager instance = new GameManager();
            _instance = instance;
        }
        return _instance;
    }
    public GameState CurrentGameState = GameState.Movement;

    public TMP_Text modeText;

    // Dialogue

    public TMP_Text dialogueText;
    public TMP_Text characterText;
    public GameObject dialogueObject;
    public GameObject characterPortraitObject;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            if (CurrentGameState == GameState.Picture) {
                CurrentGameState = GameState.Movement;
                modeText.GetComponent<TMP_Text>().text = "Movement";
            }
            else {
                CurrentGameState = GameState.Picture;
                modeText.GetComponent<TMP_Text>().text = "Picture";
            }
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            if (CurrentGameState == GameState.Dialogue) {
                CurrentGameState = GameState.Movement;
                modeText.GetComponent<TMP_Text>().text = "Movement";
            }
            else {
                CurrentGameState = GameState.Dialogue;
                modeText.GetComponent<TMP_Text>().text = "Dialogue";
            }
        }
    }

}