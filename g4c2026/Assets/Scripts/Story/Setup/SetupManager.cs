using UnityEngine;
using System.Collections.Generic;

public class SetupManager : MonoBehaviour {

    private static SetupManager _instance;

    private SetupManager() {
        _instance = this;
    }

    public static SetupManager I() {
        if (_instance == null) {
            SetupManager instance = new SetupManager();
            _instance = instance;
        }
        return _instance;
    }

    // trails
    public LineRenderer TrailLine;
    public TrailPoint CurrentTrailPoint;
    public List<TrailPoint> TrailPointEnds;
    public GameObject Player;

    // character interactions
    public List<CharacterInteraction> CharacterInteractionList;

    void Awake() {
        TrailPointEnds = new List<TrailPoint>();
        CharacterInteractionList = new List<CharacterInteraction>();
    }

    void Start() {

    }

    void Update() {
        if (CurrentTrailPoint != null) {
            var points = new Vector3[2];
            points[0] = PlayerData.PlayerPosition;
            points[1] = CurrentTrailPoint.transform.position;
            TrailLine.SetPositions(points);
        }
    }

    public void Setup(Setup setup) {
        SetupType setupType = setup.SetupType;
        List<string> arguments = setup.Arguments;
        switch (setupType) {
            case SetupType.Path:
                TrailLine.enabled = true;
                var name = arguments[0];
                for (int i = 0; i < TrailPointEnds.Count; i++) {
                    if (TrailPointEnds[i].TrailPointName == name) {
                        CurrentTrailPoint = TrailPointEnds[i];
                    }
                }
                break;
            case SetupType.Interact:
                // finding the character interaction;
                string characterInteractionName = arguments[0];
                CharacterInteraction characterInteraction = null;
                for (int i = 0; i < CharacterInteractionList.Count; i++) if (CharacterInteractionList[i].character.ToString() == characterInteractionName) characterInteraction = CharacterInteractionList[i];
                switch (arguments[1]) {
                    case "Dialogue":
                        characterInteraction.dialogue = arguments[2];
                        characterInteraction.Selectable = true;
                        break;
                    case "Give":
                        characterInteraction.giveItem = (Item)System.Enum.Parse(typeof(Item), arguments[2]);
                        characterInteraction.characterInteractionType = CharacterInteractionType.Give;
                        characterInteraction.Selectable = true;
                        break;
                    case "Inform":
                        characterInteraction.InformList = new List<ThreatSubSection>();
                        for (int i = 2; i < arguments.Count; i++) characterInteraction.InformList.Add((ThreatSubSection)System.Enum.Parse(typeof(ThreatSubSection), arguments[i]));
                        characterInteraction.characterInteractionType = CharacterInteractionType.Inform;
                        characterInteraction.Selectable = true;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

    public void TrailEnter(string s) {
        if (CurrentTrailPoint != null && s == CurrentTrailPoint.TrailPointName) {
            TrailLine.enabled = false;
            CurrentTrailPoint = null;
        }
    }

}
