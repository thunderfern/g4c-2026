using UnityEngine;

public class FoodInteraction : Interaction {

    public Item ItemType;

    void Update() {
        
    }
    
    public override void EnterRange() {
        
    }

    public override void LeaveRange()  {
        
    }

    public override void Selected() {
        PlayerData.PlayerInventory = ItemType;
        Destroy(gameObject);
    }
}
