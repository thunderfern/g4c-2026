using UnityEngine;
using System.Collections.Generic;

public class InteractionRigidbody : MonoBehaviour {

    public List<Interaction> interactions;
    public Interaction currentInteraction;

    void Start() {
        interactions = new List<Interaction>();
    }
    
    void Update() {
        if (currentInteraction && Input.GetKeyDown(KeyCode.F) && GameManager.I().CurrentGameState == GameState.Movement) {
            currentInteraction.Selected();
        }
    }

    void FixedUpdate() {

        // updating interactions

        currentInteraction = null;
        // going backwards so can delete
        for (int i = interactions.Count - 1; i >= 0; i--) {
            // game object was destroyed
            if (interactions[i] == null) {
                interactions.RemoveAt(i);
                continue;
            }
            Interaction curI = interactions[i];
            if (curI && curI.Selectable) {
                if (currentInteraction == null || (transform.position - curI.transform.position).magnitude < (transform.position - currentInteraction.transform.position).magnitude) {
                    currentInteraction = curI;
                }
            }
        }

    }

    void OnTriggerEnter(Collider collider) {
        InteractionRange interactionRange = collider.GetComponent<InteractionRange>();
        if (interactionRange) {
            interactionRange.EnterRange(tag);
            interactions.Add(interactionRange.SourceInteraction);
        }
    }

    void OnTriggerExit(Collider collider) {
        InteractionRange interactionRange = collider.GetComponent<InteractionRange>();
        if (interactionRange) {
            interactionRange.LeaveRange(tag);
            interactions.Remove(interactionRange.SourceInteraction);
        }
    }
}
