using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingUI : MonoBehaviour {
    public GameObject SettingsObj;
    public List<Slider> Sliders;

    public Button ExitButton;

    void Start() {
        ExitButton.onClick.AddListener(() => {
            GameManager.I().CurrentGameState = GameState.Movement;
        });
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameManager.I().CurrentGameState == GameState.Settings) {
                GameManager.I().CurrentGameState = GameState.Movement;
            }
            else if (GameManager.I().CurrentGameState == GameState.Movement) {
                GameManager.I().CurrentGameState = GameState.Settings;
            }
        }
        if (GameManager.I().CurrentGameState != GameState.Settings) {
            SettingsObj.SetActive(false);
            return;
        }
        SettingsObj.SetActive(true);
        for (int i = 0; i < Sliders.Count; i++) {
            AudioManager.I().AudioSettingList[i] = Sliders[i].value;
        }
    }
}
