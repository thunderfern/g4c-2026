using UnityEngine;
using System.Collections.Generic;

public class CharacterInteraction : Interaction {

    public CharacterInteractionType characterInteractionType;
    public Character character;
    public string selectionText;

    // for specific goals
    public string dialogue;
    public Item giveItem;
    public Item getItem;
    public List<ThreatSubSection> InformList;

    void Start() {
        SetupManager.I().CharacterInteractionList.Add(this);
    }

    void Update() {
        
    }
    
    public override void EnterRange() {
        GameManager.I().PerformedAction(new Goal {
            GoalType = GoalType.Enter, 
            Arguments = new List<string>() {
                character.ToString()
            }
        });
    }
    
    public override void Selected() {
        switch (characterInteractionType) {
            case CharacterInteractionType.Give:
                if (PlayerData.PlayerInventory == giveItem) {
                    PlayerData.PlayerInventory = Item.None;
                    GameManager.I().PerformedAction(new Goal {
                        GoalType = GoalType.Give, 
                        Arguments = new List<string>() {
                            character.ToString(), giveItem.ToString()
                        }
                    });
                    Selectable = false;
                }
                else ShowDialogue();
                break;
            case CharacterInteractionType.Get:
                if (PlayerData.PlayerInventory == Item.None) {
                    PlayerData.PlayerInventory = getItem;
                    // update goal
                    Selectable = false;
                }
                break;
            case CharacterInteractionType.Inform:
                bool foundAll = true;
                for (int i = 0; i < InformList.Count; i++) {
                    if (!Photobook.I().ImageCache.TryGetValue(InformList[i], out var tmp)) foundAll = false;
                }
                if (foundAll) {
                    GameManager.I().PerformedAction(new Goal {
                        GoalType = GoalType.Inform, 
                        Arguments = new List<string>() {
                            character.ToString()
                        }
                    });
                    Selectable = false;
                    // set up healing
                    GetComponent<ThreatAreaMain>().SetupHealing();
                }
                else ShowDialogue();
                break;
            default:
                ShowDialogue();
                GameManager.I().PerformedAction(new Goal {
                    GoalType = GoalType.Interact, 
                    Arguments = new List<string>() {
                        character.ToString()
                    }
                });
            break;
        }
    }

    void ShowDialogue() {
        if (dialogue == "") return;
        DialogueManager.I().UpdateDialogue(new List<DialogueInformation>() { new DialogueInformation {
            character = this.character,
            dialogue = this.dialogue,
        }});
    }
}
