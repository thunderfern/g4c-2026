using UnityEngine;
using System.Collections.Generic;

public class InteractionRigidbody : MonoBehaviour {

    List<Collider> interactions;
    Collider currentCollider;

    void Start() {
        interactions = new List<Collider>();
    }
    
    void Update()
    {
        if (currentCollider && Input.GetKeyDown(KeyCode.F))
        {
            currentCollider.GetComponent<InteractionRange>()?.SourceInteraction.Selected();
        }
    }

    void FixedUpdate() {
        if (interactions.Count != 0) currentCollider = interactions[0];
        else currentCollider = null;

        
    }

    void OnTriggerEnter(Collider collider) {
        collider.GetComponent<InteractionRange>()?.EnterRange(tag);
        interactions.Add(collider);
    }

    void OnTriggerExit(Collider collider) {
        collider.GetComponent<InteractionRange>()?.LeaveRange(tag);
        interactions.Remove(collider);
    }
}
