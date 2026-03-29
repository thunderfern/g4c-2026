using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class GameplayUI : MonoBehaviour {

    public GameObject GameplayObj;
    public Button PhotobookIcon;
    public Image InventoryImage;

    [Serializable]
    public struct ItemInspector {
        public Item Item;
        public Sprite Sprite;
    }

    public List<ItemInspector> ItemListInspector;

    void Start() {
        PhotobookIcon.onClick.AddListener(() => {
            GameManager.I().CurrentGameState = GameState.Photobook;
            GameManager.I().PerformedAction(new Goal {
                GoalType = GoalType.Open, 
                Arguments = new List<string>() {
                    "Photobook"
                }
            });
            
        });
    }

    void Update() {
        if (GameManager.I().CurrentGameState != GameState.Movement) {
            GameplayObj.SetActive(false);
            return;
        }
        GameplayObj.SetActive(true);
        InventoryImage.sprite = GetSprite(PlayerData.PlayerInventory);
    }

    Sprite GetSprite(Item item) {
        for (int i = 0; i < ItemListInspector.Count; i++) if (ItemListInspector[i].Item == item) return ItemListInspector[i].Sprite;
        return null;
    }
}