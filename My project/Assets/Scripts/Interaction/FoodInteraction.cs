using UnityEngine;

public class FoodInteraction : Interaction {
    
    public override void EnterRange() {
        Debug.Log("ok\n");
    }

    public override void LeaveRange()  {
        Debug.Log("nok\n");
    }
}
