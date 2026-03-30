using UnityEngine;

public class TrashInteraction : Interaction {

    void Update() {
        if (PlayerData.PlayerInventory == Item.RedCan || PlayerData.PlayerInventory == Item.BlueCan || PlayerData.PlayerInventory == Item.RedCanCrushed || PlayerData.PlayerInventory == Item.BlueCanCrushed) Selectable = true;
        else Selectable = false;
    }
    public override void Selected() {
        PlayerData.PlayerInventory = Item.None;
        ProgressManager.I().CurrentProgress += 10;
    }
}