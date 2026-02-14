using UnityEngine;

public class InteractionRigidbody : MonoBehaviour {

    void OnTriggerEnter(Collider collider) {
        collider.GetComponent<InteractionRange>()?.EnterRange(tag);
    }

    void OnTriggerExit(Collider collider) {
        collider.GetComponent<InteractionRange>()?.LeaveRange(tag);
    }
}
