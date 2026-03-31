using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class GameplayUI : MonoBehaviour {

    public GameObject GameplayObj;
    public Button PhotobookIcon;
    public Button SettingsIcon;
    public Button PhotoIcon;
    public Image InventoryImage;
    public TMP_Text ProgressIndicator;

    [Serializable]
    public struct ItemInspector {
        public Item Item;
        public Sprite Sprite;
    }

    public List<ItemInspector> ItemListInspector;

    void Start() {
        PhotobookIcon.onClick.AddListener(() => {
            GameManager.I().CurrentGameState = GameState.Photobook;
        });
        SettingsIcon.onClick.AddListener(() => {
            GameManager.I().CurrentGameState = GameState.Settings;
        });
        PhotoIcon.onClick.AddListener(() => {
            PhotoManager.I().EnterPhotoMode();
        });
    }

    void Update() {
        if (GameManager.I().CurrentGameState != GameState.Movement) {
            GameplayObj.SetActive(false);
            return;
        }
        GameplayObj.SetActive(true);
        InventoryImage.sprite = GetSprite(PlayerData.PlayerInventory);
        ProgressIndicator.text = "Progress: " + Math.Round((float)ProgressManager.I().CurrentProgress * 100 / (float)ProgressManager.I().MaxProgress, 2) + "%";
    }

    Sprite GetSprite(Item item) {
        for (int i = 0; i < ItemListInspector.Count; i++) if (ItemListInspector[i].Item == item) return ItemListInspector[i].Sprite;
        return null;
    }
}