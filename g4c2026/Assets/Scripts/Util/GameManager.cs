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
    
    public List<StoryGoals> GoalList;

    public TMP_Text modeText;

    void Start() {
        GoalList = new List<StoryGoals>();
        StartStorySection("New Beginnings 1");
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
        if (StoryManager.StoryGoal.TryGetValue(storyName, out var goalList)) {
            UpdateGoals(storyName, goalList);
        }
        
    }

    public void UpdateGoals(string storyName, List<Goal> goalList) {
        StoryGoals tmp = new StoryGoals {
            StoryName = storyName,
            Goals = goalList,
        };
        GoalList.Add(tmp);
        GoalListUI.I().UpdateUI();
    }

    public void PerformedAction(Goal goal) {
        for (int i = 0; i < GoalList.Count; i++) {
            StoryGoals curList = GoalList[i];
            int idx = -1;
            for (int j = 0; j < curList.Goals.Count; j++) if (curList.Goals[j].Equals(goal)) idx = j;
            if (idx != -1) {
                GoalList[i].Goals.RemoveAt(idx);
            }
            // finished all goals, can start next section
            Debug.Log("linx" + curList.Goals.Count);
            if (curList.Goals.Count == 0) {
                if (StoryManager.StoryNext.TryGetValue(curList.StoryName, out var nextStoryList)) {
                    foreach (var nextStory in nextStoryList) {
                        StartStorySection(nextStory);
                    }
                }
            }
        }
        GoalListUI.I().UpdateUI();
    }
}