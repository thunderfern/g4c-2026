using UnityEngine;
using TMPro;

public class PhotoManager : MonoBehaviour {

    public int resWidth = 2550; 
    public int resHeight = 3300;
    public Camera camera;

    public float moveSensitivity = 1.0f;
    public float rotateSensitivity = 1.0f;
    public float zoomSensitivity = 1.0f;

    public TMP_Text aimingText;
    
    void Update() {
        
        if (GameManager.I().CurrentGameState != GameState.Picture) return;

        MoveCamera();
        ZoomCamera();
        SenseObjects();
        //RotateCamera();

        if (Input.GetKeyDown(KeyCode.Space)) {
            TakePhoto();
        }
    }

    void MoveCamera() {
        float deltaTime = Time.deltaTime;
        Vector3 newCameraPos = camera.transform.position;
        if (Input.GetKey(KeyCode.W)) newCameraPos += new Vector3(0, deltaTime * 1.0f, 0) * moveSensitivity;
        if (Input.GetKey(KeyCode.A)) newCameraPos += new Vector3(deltaTime * -1.0f, 0, 0) * moveSensitivity;
        if (Input.GetKey(KeyCode.S)) newCameraPos += new Vector3(0, deltaTime * -1.0f, 0) * moveSensitivity;
        if (Input.GetKey(KeyCode.D)) newCameraPos += new Vector3(deltaTime * 1.0f, 0, 0) * moveSensitivity;
        camera.transform.position = newCameraPos;
    }

    /*void RotateCamera() {

    }*/

    void ZoomCamera() {
        float deltaTime = Time.deltaTime;
        Vector3 newCameraPos = camera.transform.position + new Vector3(0, 0, Input.mouseScrollDelta.y) * zoomSensitivity;
        camera.transform.position = newCameraPos;
    }

    void SenseObjects() {
        bool found = false;
        PhotoCandidate[] photoCandidates = FindObjectsByType<PhotoCandidate>(FindObjectsSortMode.None);
        for (int i = 0; i < photoCandidates.Length; i++) {
            if (photoCandidates[i].gameObject.name == "box") {
                Debug.Log(camera.WorldToViewportPoint(photoCandidates[i].gameObject.transform.position));
                Debug.Log(photoCandidates[i].gameObject.transform.position);
            }
            if (camera.WorldToViewportPoint(photoCandidates[i].gameObject.transform.position).x > 0 && camera.WorldToViewportPoint(photoCandidates[i].gameObject.transform.position).x < 1 && camera.WorldToViewportPoint(photoCandidates[i].gameObject.transform.position).y > 0 && camera.WorldToViewportPoint(photoCandidates[i].gameObject.transform.position).y < 1) {
                aimingText.GetComponent<TMP_Text>().text = photoCandidates[i].gameObject.name;
                found = true;
                break;
            }
        }
        if (!found) aimingText.GetComponent<TMP_Text>().text = "None";
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
        string filename = "hi.png";
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));
    }
}