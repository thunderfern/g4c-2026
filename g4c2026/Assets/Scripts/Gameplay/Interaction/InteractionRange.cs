using UnityEngine;

public class InteractionRange : MonoBehaviour {
    
    [HideInInspector] public GameObject Source;

    [HideInInspector] public Interaction SourceInteraction;

    void Start() {
        SourceInteraction = Source.GetComponent<Interaction>();
    }

    public void EnterRange(string tag) {
        SourceInteraction.EnterRange();
    }

    public void LeaveRange(string tag) {
        SourceInteraction.LeaveRange();
    }
}
