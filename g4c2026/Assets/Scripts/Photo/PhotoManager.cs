using UnityEngine;
using TMPro;
using System;
using System.IO;
using System.Collections.Generic;

public class PhotoManager : MonoBehaviour {

    private static PhotoManager _instance;

    private PhotoManager() {
        _instance = this;
    }

    public static PhotoManager I() {
        if (_instance == null) {
            PhotoManager instance = new PhotoManager();
            _instance = instance;
        }
        return _instance;
    }

    // camera settings

    private int resWidth = 4000; 
    private int resHeight = 4000;
    public Camera camera;
    public Camera PlayerCamera;

    public float moveSensitivity = 1.0f;
    public float rotateSensitivity = 1.0f;
    public float zoomSensitivity = 1.0f;

    // aiming
    public TMP_Text aimingText;
    private PhotoCandidate photoCandidate;
    private Vector3 CameraDelta;
    private Transform CameraOrigin;

    void Start() {
        CameraDelta = new();
        CameraOrigin = PlayerCamera.transform;
    }
    
    void Update() {
        
        if (GameManager.I().CurrentGameState != GameState.Picture) return;

        MoveCamera();
        SenseObjects();

        if (Input.GetKeyDown(KeyCode.Space)) {
            TakePhoto();
        }
    }

    public void ResetDelta() {
        CameraDelta = new();
    }


    // Taking Photos
    void MoveCamera() {
        float deltaTime = Time.deltaTime;
        if (Input.GetKey(KeyCode.W)) CameraDelta += new Vector3(0, deltaTime * 1.0f, 0) * moveSensitivity;
        if (Input.GetKey(KeyCode.A)) CameraDelta += new Vector3(deltaTime * -1.0f, 0, 0) * moveSensitivity;
        if (Input.GetKey(KeyCode.S)) CameraDelta += new Vector3(0, deltaTime * -1.0f, 0) * moveSensitivity;
        if (Input.GetKey(KeyCode.D)) CameraDelta += new Vector3(deltaTime * 1.0f, 0, 0) * moveSensitivity;
        // zooming
        CameraDelta += new Vector3(0, 0, Input.mouseScrollDelta.y) * zoomSensitivity;
        CameraDelta = new Vector3(Math.Min(Math.Max(CameraDelta.x, -3), 3), Math.Min(Math.Max(CameraDelta.y, -3), 3), Math.Min(Math.Max(CameraDelta.z, -3), 3));
        camera.transform.position = CameraOrigin.transform.position + CameraDelta.z * CameraOrigin.forward.normalized + CameraDelta.y * CameraOrigin.up.normalized + CameraDelta.x * CameraOrigin.right.normalized;
    }

    void SenseObjects() {
        photoCandidate = null;
        PhotoCandidate[] photoCandidates = FindObjectsByType<PhotoCandidate>(FindObjectsSortMode.None);
        float distance = -1.0f;
        // prioritize the ones that are closer to the player
        for (int i = 0; i < photoCandidates.Length; i++) {
            float viewpointX = camera.WorldToViewportPoint(photoCandidates[i].transform.position).x;
            float viewpointY = camera.WorldToViewportPoint(photoCandidates[i].transform.position).y;
            float viewpointZ = camera.WorldToViewportPoint(photoCandidates[i].transform.position).z;
            if (viewpointX > 0 && viewpointX < 1 && viewpointY > 0 && viewpointY < 1 && viewpointZ > 0.5) {
                if (distance == -1 || (photoCandidates[i].transform.position - camera.transform.position).magnitude < distance) {
                    photoCandidate = photoCandidates[i];
                    distance = (photoCandidates[i].transform.position - camera.transform.position).magnitude;
                }
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
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false, false);
        camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        screenShot.Apply();
        Photobook.I().ImageCache[photoCandidate.ThreatSubSection] = screenShot;
        camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = GetScreenshotName();
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));

        //Photobook.I().ImageCache[photoCandidate.ThreatSubSection] = screenShot;

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