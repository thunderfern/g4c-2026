using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public struct DialogueInformation {
    public Character character;
    public string dialogue;
}

public struct CharacterDisplay {
    public string characterName;
    public Color characterBackground;
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
    public GameObject characterBackgroundObject;

    // dialogue customizations
    [Serializable]
    public struct CharacterDisplayInspector {
        public Character character;
        public Color characterBackground;
        public string characterName;
    }
    
    // inspector initializers
    public List<CharacterDisplayInspector> CharacterNameBackground;

    private List<DialogueInformation> dialogueStream;
    private Dictionary<Character, CharacterDisplay> characterDisplayLookup;
    private float curTextTimer;

    void Awake() {
        characterDisplayLookup = new Dictionary<Character, CharacterDisplay>();
        for (int i = 0; i < CharacterNameBackground.Count; i++) {
            characterDisplayLookup[CharacterNameBackground[i].character] = new CharacterDisplay() {
                characterName = CharacterNameBackground[i].characterName,
                characterBackground = CharacterNameBackground[i].characterBackground,
            };
        }
    }

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
        else GameManager.I().CurrentGameState = GameState.Movement;
    }

    public void UpdateDialogue(List<DialogueInformation> dialogueStream) {
        dialogueObject.SetActive(true);
        this.dialogueStream = dialogueStream;
        display();
    }

    void display() {
        if (dialogueStream.Count == 0) return;

        // background display
        characterBackgroundObject.GetComponent<UnityEngine.UI.Image>().color = characterDisplayLookup[dialogueStream[0].character].characterBackground;

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