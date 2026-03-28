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
    public GameState CurrentGameState = GameState.MainMenu;
    
    public List<StoryGoals> GoalList;

    public TMP_Text modeText;

    private bool startedStory = false;

    public Camera PlayerCamera;
    public Camera MainMenuCamera;

    void Start() {
        GoalList = new List<StoryGoals>();
        PlayerCamera.enabled = false;
        MainMenuCamera.enabled = true;
    }

    void Update() {
        if (CurrentGameState == GameState.MainMenu) {
            if (Input.anyKeyDown) {
                PlayerCamera.enabled = true;
                MainMenuCamera.enabled = false;
                CurrentGameState = GameState.Movement;
                StartStorySection("New Beginnings 1");
            }
        }
        // if (!startedStory) {
        //     //StartStorySection("The First Healing 1");
        //     StartStorySection("New Beginnings 7");
        //     startedStory = true;
        // }
        if (Input.GetKeyDown(KeyCode.Z)) {
            if (CurrentGameState == GameState.Picture) {
                PhotoManager.I().LeavePhotoMode();
            }
            else if (CurrentGameState == GameState.Movement) {
                PhotoManager.I().EnterPhotoMode();
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
            DialogueManager.I().UpdateDialogue(dialogueInformation);
        }
        if (StoryManager.StoryGoal.TryGetValue(storyName, out var goalList)) {
            UpdateGoals(storyName, goalList);
        }
        if (StoryManager.StorySetup.TryGetValue(storyName, out var setupList)) {
            UpdateSetup(setupList);
        }
        
    }

    public void UpdateSetup(List<Setup> setupList) {
        foreach (Setup s in setupList) {
            SetupManager.I().Setup(s);
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
        if (goal.GoalType == GoalType.Enter) SetupManager.I().TrailEnter(goal.Arguments[0]);
        for (int i = 0; i < GoalList.Count; i++) {
            StoryGoals curList = GoalList[i];
            int idx = -1;
            for (int j = 0; j < curList.Goals.Count; j++) if (curList.Goals[j].Equals(goal)) idx = j;
            if (idx != -1) {
                GoalList[i].Goals.RemoveAt(idx);
            }
            // finished all goals, can start next section
            if (curList.Goals.Count == 0) {
                // removes the list if it is empty
                GoalList.RemoveAt(i);
                i--;
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