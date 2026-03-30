using UnityEngine;

public class TreeOccupant : ThreatAreaOccupantsMain {

    TreeMain treeMain;

    void Awake() {
        treeMain = GetComponent<TreeMain>();
    }

    void Update() {

    }

    public override void Heal() {
        base.Heal();
        treeMain.TreeState = TreeState.Growing;
    }
}