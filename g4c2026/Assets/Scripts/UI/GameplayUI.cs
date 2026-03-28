using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameplayUI : MonoBehaviour {

    public GameObject GameplayObj;
    public Button PhotobookIcon;

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
        if (GameManager.I().CurrentGameState == GameState.Movement) GameplayObj.SetActive(true);
        else GameplayObj.SetActive(false);
    }
}