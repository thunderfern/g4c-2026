using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GoalListUI : MonoBehaviour {

    // Singleton
    private static GoalListUI _instance;

    private GoalListUI() {
        _instance = this;
    }

    public static GoalListUI I() {
        if (_instance == null) {
            GoalListUI instance = new GoalListUI();
            _instance = instance;
        }
        return _instance;
    }

    public List<GameObject> CurrentGoalList;
    public GameObject GoalListObject;
    public GameObject GoalListEntry;

    public void UpdateUI() {
        for (int i = 0; i < CurrentGoalList.Count; i++) {
            Destroy(CurrentGoalList[i]);
        }
        List<StoryGoals> goalList = GameManager.I().GoalList;
        for (int i = 0; i < goalList.Count; i++) {
            List<Goal> curEntry = goalList[i].Goals;
            for (int j = 0; j < curEntry.Count; j++) {
                GameObject tmp = Instantiate(GoalListEntry, GoalListObject.transform);
                tmp.GetComponent<TMP_Text>().text = curEntry[j].GoalDescription;
                CurrentGoalList.Add(tmp);
            }
        }
    }
}