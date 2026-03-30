using UnityEngine;

public class ProgressManager : MonoBehaviour {
    private static ProgressManager _instance;

    private ProgressManager() {
        _instance = this;
    }

    public static ProgressManager I() {
        if (_instance == null) {
            ProgressManager instance = new ProgressManager();
            _instance = instance;
        }
        return _instance;
    }

    public int MaxProgress;
    public int CurrentProgress;
    
    void Start() {
        CurrentProgress = 0;
    }

    void Update() {
        
    }
}