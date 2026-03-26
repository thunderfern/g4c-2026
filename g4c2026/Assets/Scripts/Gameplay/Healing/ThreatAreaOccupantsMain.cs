using UnityEngine;

public class ThreatAreaOccupantsMain : MonoBehaviour {
    PhotoCandidate photoCandidate;

    public ThreatSubSection BeforeThreatSubSection;
    public ThreatSubSection AfterThreatSubSection;
    public string BeforePictureDescription;
    public string AfterPictureDescription;

    void Start() {
        photoCandidate = GetComponent<PhotoCandidate>();
        photoCandidate.ThreatSubSection = BeforeThreatSubSection;
        photoCandidate.PhotoTitle = BeforePictureDescription;
    }

    public void Heal() {
        photoCandidate.ThreatSubSection = AfterThreatSubSection;
        photoCandidate.PhotoTitle = AfterPictureDescription;
    }

}