using UnityEngine;
using TMPro;
using System.Collections.Generic;

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
    
    public List<Goal> GoalList;

    public TMP_Text modeText;



    void Start() {
        StartStorySection("The Beginning");
    }

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
            else if (CurrentGameState == GameState.Movement) {
                CurrentGameState = GameState.Dialogue;
                modeText.GetComponent<TMP_Text>().text = "Dialogue";
            }
        }

        if (Input.GetKeyDown(KeyCode.B)) {
            if (CurrentGameState == GameState.Photobook) {
                CurrentGameState = GameState.Movement;
                modeText.GetComponent<TMP_Text>().text = "Movement";
            }
            else if (CurrentGameState == GameState.Movement) {
                CurrentGameState = GameState.Photobook;
                modeText.GetComponent<TMP_Text>().text = "Photobook";
            }
        }
    }

    public void StartStorySection(string storyName) {
        if (StoryManager.StoryDialogue.TryGetValue(storyName, out var dialogueInformation)) {
            CurrentGameState = GameState.Dialogue;
            DialogueManager.I().UpdateDialogue(dialogueInformation);
        }
        UpdateGoals();

    }

    public void UpdateGoals() {

    }

    public void PerformedAction(Goal goal) {
        int idx = GoalList.IndexOf(goal);
        if (idx != -1) {
            GoalList.RemoveAt(idx);
            // todo add update frontend ui
        }
    }
}