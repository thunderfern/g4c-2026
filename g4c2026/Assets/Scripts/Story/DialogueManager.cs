using UnityEngine;
using System.Collections.Generic;
using TMPro;

public struct DialogueInformation {
    public Character character;
    public string dialogue;
}

public struct CharacterDisplay {
    public string characterName;
}

public class DialogueManager : MonoBehaviour {

    // Singleton
    private static DialogueManager _instance;

    private DialogueManager() {
        _instance = this;
    }

    public static DialogueManager I() {
        if (_instance == null) {
            DialogueManager instance = new DialogueManager();
            _instance = instance;
        }
        return _instance;
    }

    // UI refernces
    public TMP_Text dialogueText;
    public TMP_Text characterText;
    public GameObject dialogueObject;

    // dialogue customizations
    public struct CharacterDisplayInspector {
        Character character;
        CharacterBackground characterBackground;
        string displayName;
    }
    
    // inspector initializers
    public List<CharacterDisplayInspector> CharacterNameBackground;

    private List<DialogueInformation> dialogueStream;
    private Dictionary<Character, CharacterDisplay> characterDisplayLookup;
    private float curTextTimer;

    void Update() {
        if (GameManager.I().CurrentGameState != GameState.Dialogue) return;
        if (dialogueStream.Count > 0) {
            if (dialogueText.GetComponent<TMP_Text>().maxVisibleCharacters < dialogueStream[0].dialogue.Length) {
                curTextTimer += Time.deltaTime;
                dialogueText.GetComponent<TMP_Text>().maxVisibleCharacters = (int)(curTextTimer * 20);
                if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)) dialogueText.GetComponent<TMP_Text>().maxVisibleCharacters = dialogueStream[0].dialogue.Length;
            }
            else if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)) displayNext();
        }
    }

    public void UpdateDialogue(List<DialogueInformation> dialogueStream) {
        this.dialogueStream = dialogueStream;
        display();
    }

    void display() {
        if (dialogueStream.Count == 0) return;

        // text display
        characterText.GetComponent<TMP_Text>().text = characterDisplayLookup[dialogueStream[0].character].characterName;
        dialogueText.GetComponent<TMP_Text>().text = dialogueStream[0].dialogue;
        dialogueText.GetComponent<TMP_Text>().maxVisibleCharacters = 0;
        curTextTimer = 0;
    }

    void displayNext() {
        dialogueStream.RemoveAt(0);
        if (dialogueStream.Count == 0) {
            dialogueObject.SetActive(false);
            return;
        }
        display();
    }

}