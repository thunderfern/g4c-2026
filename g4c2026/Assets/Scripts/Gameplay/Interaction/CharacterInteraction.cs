using UnityEngine;
using System.Collections.Generic;

public class CharacterInteraction : Interaction {

    CharacterInteractionType characterInteractionType;
    public Character character;
    public string selectionText;

    // for specific goals
    public string dialogue;
    public Item giveItem;
    public Item getItem;

    void Update() {
        
    }
    
    public override void Selected() {
        Debug.Log("selected!");
        switch (characterInteractionType) {
            case CharacterInteractionType.Give:
                if (PlayerData.PlayerInventory == giveItem) {
                    PlayerData.PlayerInventory = Item.None;
                    GameManager.I().PerformedAction(new Goal {
                        GoalType = GoalType.Give, 
                        Arguments = new List<string>() {
                            giveItem.ToString()
                        }
                    });
                }
                break;
            case CharacterInteractionType.Get:
                if (PlayerData.PlayerInventory == Item.None) {
                    PlayerData.PlayerInventory = getItem;
                    // update goal
                }
                break;
            default:
                GameManager.I().PerformedAction(new Goal {
                    GoalType = GoalType.Interact, 
                    Arguments = new List<string>() {
                        character.ToString()
                    }
                });
            break;
        }
    }
}
