using UnityEngine;

public class BirdOccupant : ThreatAreaOccupantsMain {

    CharacterInteraction characterInteraction;

    public GameObject nest;

    void Awake() {
        characterInteraction = GetComponent<CharacterInteraction>();
    }

    void Update() {

    }

    public override void Heal() {
        base.Heal();
        nest.SetActive(true);
    }
}