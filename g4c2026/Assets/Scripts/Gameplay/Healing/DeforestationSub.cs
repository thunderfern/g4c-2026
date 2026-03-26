using UnityEngine;

public class DeforestationSub : ThreatAreaSubMain {

    TreeInteraction treeInteraction;

    void Start() {
        treeInteraction = GetComponent<TreeInteraction>();
    }

    void Update() {
        if (GetComponent<TreeMain>().TreeState == TreeState.Growing) Healed = true;
    }

    public override void SetupHealing() {
        treeInteraction.treeInteractionType = TreeInteractionType.Grow;
    }
}