using UnityEngine;
using System.Collections.Generic;

public class TrailPoint : MonoBehaviour {
    public string TrailPointName;

    void Start() {
        SetupManager.I().TrailPointEnds.Add(this);
    }

}
