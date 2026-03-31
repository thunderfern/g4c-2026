using UnityEngine;

public class BirdOccupant : ThreatAreaOccupantsMain {

    CharacterInteraction characterInteraction;

    public GameObject nest;

    void Awake() {
        characterInteraction = GetComponent<CharacterInteraction>();
    }

    public override void Start() {
        base.Start();
        nest.SetActive(false);
    }

    void Update() {

    }

    public override void Heal() {
        base.Heal();
        nest.SetActive(true);
    }
}