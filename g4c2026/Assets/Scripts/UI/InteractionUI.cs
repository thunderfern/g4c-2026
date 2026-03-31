using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour {
    public InteractionRigidbody interactionRigidbody;

    public TMP_Text InteractionText;

    public GameObject InteractionObject;

    void Update() {
        if (interactionRigidbody.currentInteraction && GameManager.I().CurrentGameState == GameState.Movement) {
            if (interactionRigidbody.currentInteraction.SelectableText == "") InteractionText.GetComponent<TMP_Text>().text = "Interact";
            else InteractionText.GetComponent<TMP_Text>().text = interactionRigidbody.currentInteraction.SelectableText;
            InteractionObject.SetActive(true);
        }
        else InteractionObject.SetActive(false);
    }
}