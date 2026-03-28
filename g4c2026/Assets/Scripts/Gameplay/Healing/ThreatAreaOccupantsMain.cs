using UnityEngine;

public class ThreatAreaOccupantsMain : MonoBehaviour {
    PhotoCandidate photoCandidate;

    public ThreatSubSection BeforeThreatSubSection;
    public ThreatSubSection AfterThreatSubSection;

    void Start() {
        photoCandidate = GetComponent<PhotoCandidate>();
        photoCandidate.ThreatSubSection = BeforeThreatSubSection;
    }

    public void Heal() {
        photoCandidate.ThreatSubSection = AfterThreatSubSection;
    }

}