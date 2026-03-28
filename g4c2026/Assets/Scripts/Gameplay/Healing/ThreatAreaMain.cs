using UnityEngine;
using System.Collections.Generic;

public class ThreatAreaMain : MonoBehaviour {
    public List<ThreatAreaSubMain> threatAreaSubs;
    public List<ThreatAreaOccupantsMain> threatAreaOccupants;
    public ThreatSection ThreatSection;
    public ThreatSubSection ThreatSectionAfter;

    public bool healed = false;

    void Start() {
        // to do: make the healing happen after informing.
        //SetupHealing();
    }

    void Update() {
        if (!healed) {
            bool allHealed = true;
            for (int i = 0; i < threatAreaSubs.Count; i++) {
                if (!threatAreaSubs[i].Healed) allHealed = false;
            }
            if (allHealed) {
                for (int i = 0; i < threatAreaOccupants.Count; i++) {
                    threatAreaOccupants[i].Heal();
                }
                healed = true;
                GetComponent<PhotoCandidate>().ThreatSubSection = ThreatSectionAfter;
                GameManager.I().PerformedAction(new Goal {
                    GoalType = GoalType.Heal, 
                    Arguments = new List<string>() {
                        ThreatSection.ToString()
                    }
                });
            }
        }
    }

    public void SetupHealing() {
        for (int i = 0; i < threatAreaSubs.Count; i++) {
            threatAreaSubs[i].SetupHealing();
        }
    }
}