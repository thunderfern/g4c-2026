using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;

public class PhotoManager : MonoBehaviour {

    // camera settings

    public int resWidth = 2550; 
    public int resHeight = 3300;
    public Camera camera;

    public float moveSensitivity = 1.0f;
    public float rotateSensitivity = 1.0f;
    public float zoomSensitivity = 1.0f;

    // aiming
    public TMP_Text aimingText;
    private PhotoCandidate photoCandidate;
    
    void Update() {
        
        if (GameManager.I().CurrentGameState != GameState.Picture) return;

        MoveCamera();
        ZoomCamera();
        SenseObjects();

        if (Input.GetKeyDown(KeyCode.Space)) {
            TakePhoto();
        }
    }


    // Taking Photos
    void MoveCamera() {
        float deltaTime = Time.deltaTime;
        Vector3 newCameraPos = camera.transform.position;
        if (Input.GetKey(KeyCode.W)) newCameraPos += new Vector3(0, deltaTime * 1.0f, 0) * moveSensitivity;
        if (Input.GetKey(KeyCode.A)) newCameraPos += new Vector3(deltaTime * -1.0f, 0, 0) * moveSensitivity;
        if (Input.GetKey(KeyCode.S)) newCameraPos += new Vector3(0, deltaTime * -1.0f, 0) * moveSensitivity;
        if (Input.GetKey(KeyCode.D)) newCameraPos += new Vector3(deltaTime * 1.0f, 0, 0) * moveSensitivity;
        camera.transform.position = newCameraPos;
    }

    void ZoomCamera() {
        float deltaTime = Time.deltaTime;
        Vector3 newCameraPos = camera.transform.position + new Vector3(0, 0, Input.mouseScrollDelta.y) * zoomSensitivity;
        camera.transform.position = newCameraPos;
    }

    void SenseObjects() {
        photoCandidate = null;
        PhotoCandidate[] photoCandidates = FindObjectsByType<PhotoCandidate>(FindObjectsSortMode.None);
        for (int i = 0; i < photoCandidates.Length; i++) {
            if (camera.WorldToViewportPoint(photoCandidates[i].gameObject.transform.position).x > 0 && camera.WorldToViewportPoint(photoCandidates[i].gameObject.transform.position).x < 1 && camera.WorldToViewportPoint(photoCandidates[i].gameObject.transform.position).y > 0 && camera.WorldToViewportPoint(photoCandidates[i].gameObject.transform.position).y < 1) {
                photoCandidate = photoCandidates[i];
                break;
            }
        }
        if (!photoCandidate) aimingText.GetComponent<TMP_Text>().text = "None";
        else {
            aimingText.GetComponent<TMP_Text>().text = photoCandidate.PhotoTitle;
        }
    }

    void TakePhoto() {
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = GetScreenshotName();
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));

        GameManager.I().PerformedAction(new Goal {
            GoalType = GoalType.Picture, 
            Arguments = new List<string>() {
                photoCandidate.ThreatSubSection.ToString()
            }
        });
    }

    string GetScreenshotName() {
        string folderPath = "Screenshots/";
        if (photoCandidate) folderPath += photoCandidate.ThreatSection.ToString() + "/" + photoCandidate.ThreatSubSection.ToString();
        else folderPath += "All";
        if (!Directory.Exists(folderPath)) {
            Directory.CreateDirectory(folderPath);
        }
        int fileCount = Directory.GetFiles(folderPath).Length;
        return folderPath + "/Screenshot" + fileCount + ".png";
    }

    // Management

    // storage

    public static List<PhotoCandidate> PhotoCandidates = new List<PhotoCandidate>();

    public static void RegisterPhotoCandidate(PhotoCandidate photoCandidate) {
        PhotoCandidates.Add(photoCandidate);
    }

    public static ThreatSection GetThreatSection(ThreatSubSection threatSubSection) {
        for (int i = 0 ; i < PhotoCandidates.Count; i++) {
            if (PhotoCandidates[i].ThreatSubSection == threatSubSection) return PhotoCandidates[i].ThreatSection;
        }
        return ThreatSection.None;
    }

}