using UnityEngine;

public class BirdOccupant : ThreatAreaOccupantsMain {

    CharacterInteraction characterInteraction;

    void Awake() {
        characterInteraction = GetComponent<CharacterInteraction>();
    }

    void Update() {

    }

    public override void Heal() {
        base.Heal();
        characterInteraction.giveItem = Item.Stick;
        characterInteraction.characterInteractionType = CharacterInteractionType.Give;
        characterInteraction.Selectable = true;
    }
}