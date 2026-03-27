using UnityEngine;

public class Interaction : MonoBehaviour {
    
    public bool Interactable;
    public bool Selectable;
    public string SelectableText;
    public GameObject InteractionRange;

    void Awake() {
        InteractionRange.GetComponent<InteractionRange>().Source = gameObject;
    }

    public virtual void EnterRange() {
        
    }

    public virtual void LeaveRange() {
        
    }

    public virtual void Selected() {
        
    }

}
