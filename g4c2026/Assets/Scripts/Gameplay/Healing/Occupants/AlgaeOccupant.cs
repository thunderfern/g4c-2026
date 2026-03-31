using UnityEngine;
using System.Collections.Generic;

public class AlgaeOccupant : ThreatAreaOccupantsMain {

    public List<GameObject> AlgaeChildren;

    void Awake() {

    }

    void Update() {

    }

    public override void Heal() {
        base.Heal();
        for (int i = 0; i < AlgaeChildren.Count; i++) {
            if (i % 3 != 0) Destroy(AlgaeChildren[i]);
        }
    }
}