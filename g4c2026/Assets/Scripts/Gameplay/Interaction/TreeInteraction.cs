using UnityEngine;

public class TreeInteraction : Interaction {

    public TreeInteractionType treeInteractionType = TreeInteractionType.None;

    TreeMain treeMain;

    void Start() {
        treeMain = GetComponent<TreeMain>();
    }

    public override void Selected() {
        switch (treeInteractionType) {
            case TreeInteractionType.Grow:
                treeMain.TreeState = TreeState.Growing;
                break;
            default:
                break;
        }
    }
}