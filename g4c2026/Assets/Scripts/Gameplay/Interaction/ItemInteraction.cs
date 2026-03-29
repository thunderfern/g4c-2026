using UnityEngine;

public class ItemInteraction : Interaction {

    public Item ItemType;

    void Update() {
        
    }
    
    public override void EnterRange() {
        
    }

    public override void LeaveRange()  {
        
    }

    public override void Selected() {
        if (PlayerData.PlayerInventory != Item.None) return;
        PlayerData.PlayerInventory = ItemType;
        Destroy(gameObject);
    }
}
