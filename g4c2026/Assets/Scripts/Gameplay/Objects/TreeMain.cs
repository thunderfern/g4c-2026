using UnityEngine;
using System.Collections.Generic;

public enum TreeState {
    Deforested,
    Growing,
    Grown,
}
public class TreeMain : MonoBehaviour {
    public TreeState TreeState;

    public List<GameObject> TreeStages;

    private float currentGrowTime = 0;
    public int currentStage = 2;

    void Update() {
        if (TreeState == TreeState.Growing) {
            if (currentStage == 0) {
                currentStage = 1;
                TreeStages[0].SetActive(false);
                TreeStages[1].SetActive(true);
            }
            currentGrowTime += Time.deltaTime;
            if (currentGrowTime >= 10f) {
                currentStage++;
                if (currentStage == TreeStages.Count) TreeState = TreeState.Grown;
                else {
                    TreeStages[currentStage - 1].SetActive(false);
                    TreeStages[currentStage].SetActive(true);
                }
                currentGrowTime = 0;
            }
        }
    }
}