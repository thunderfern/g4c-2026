using UnityEngine;

public class FoodInteraction : Interaction {

    void Update()
    {
        
    }
    
    public override void EnterRange() {
        Debug.Log("ok\n");
    }

    public override void LeaveRange()  {
        Debug.Log("nok\n");
    }
}
