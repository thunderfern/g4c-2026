using UnityEngine;

public class DeforestationSub : ThreatAreaSubMain {

    TreeInteraction treeInteraction;

    void Awake() {
        treeInteraction = GetComponent<TreeInteraction>();
    }

    void Update() {
        if (GetComponent<TreeMain>().TreeState == TreeState.Growing) Healed = true;
    }

    public override void SetupHealing() {
        treeInteraction.RegrowTree();
    }
}