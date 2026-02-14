using UnityEngine;

public class InteractionRange : MonoBehaviour {
    
    public GameObject Source;

    private Interaction sourceInteraction;

    void Start() {
        sourceInteraction = Source.GetComponent<Interaction>();
    }

    public void EnterRange(string tag) {
        sourceInteraction.EnterRange();
    }

    public void LeaveRange(string tag) {
        sourceInteraction.LeaveRange();
    }
}
