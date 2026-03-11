using UnityEngine;
using System.Collections.Generic;

public class StoryManager : MonoBehaviour {

    private static StoryManager _instance;

    private StoryManager() {
        _instance = this;
    }

    public static StoryManager I() {
        if (_instance == null) {
            StoryManager instance = new StoryManager();
            _instance = instance;
        }
        return _instance;
    }

    public List<TextAsset> StoryFiles;

    public static Dictionary<string, List<DialogueInformation>> StoryDialogue;
    public static Dictionary<string, List<Goal>> StoryGoal;
    public static Dictionary<string, List<string>> StoryNext;

    void Awake() {
        StoryDialogue = new Dictionary<string, List<DialogueInformation>>();
        for (int sf = 0; sf < StoryFiles.Count; sf++) {
            // setup
            var file = StoryFiles[sf];
            string fullText = file.text;
            string[] lines = fullText.Split('\n');

            string currentName = "";
            for (int i = 0; i < lines.Length; i++) {
                // whitespace
                lines[i] = lines[i].Trim();
                if (lines[i] == "") continue;

                List<string> parsedLine = StringUtil.ParseStoryLine(lines[i]);
                switch (parsedLine[0]) {
                    case "Name":
                        currentName = parsedLine[1];
                        break;
                    case "Goal":
                        GoalType goalType = (GoalType)System.Enum.Parse(typeof(GoalType), parsedLine[0]);
                        parsedLine.RemoveAt(0);
                        Goal goal = new Goal() {
                            character = goalType,
                            dialogue = parsedLine,
                        };
                        if (!StoryGoal.TryGetValue(currentName, out var tmp)) StoryGoal[currentName] = new List<Goal>();
                        StoryGoal[currentName].Add(goal);
                        break;
                    case "Next":
                        if (!StoryNext.TryGetValue(currentName, out var tmp)) StoryNext[currentName] = new List<DialogueInformation>();
                        StoryNext[currentName] = parsedLine[1];
                        break;
                    case "Setup":
                        break;
                    default:
                        // this is character dialogue
                        DialogueInformation dialogueInformation = new DialogueInformation() {
                            character = (Character)System.Enum.Parse(typeof(Character), parsedLine[0]),
                            dialogue = parsedLine[1],
                        };
                        // todo: out of bounds checking
                        // checking if the thing exists in StoryDialogue
                        if (!StoryDialogue.TryGetValue(currentName, out var tmp)) StoryDialogue[currentName] = new List<DialogueInformation>();
                        StoryDialogue[currentName].Add(dialogueInformation);
                        break;
                }
            }
        }
    }
}
