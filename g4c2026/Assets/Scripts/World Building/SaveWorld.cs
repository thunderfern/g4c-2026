using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveWorld : MonoBehaviour {
    public Button SaveWorldButton;
    public string SaveWorldName = "";
    private const string SavePath = "Assets/Resources/";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (SaveWorldName == "") SaveWorldName = "SaveWorld";

        SaveWorldButton.onClick.AddListener(() => {

            // getting all the objects to save
            SaveObject[] saveObjects = FindObjectsByType<SaveObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            // setting file name
            string FinalSavePath = SavePath + SaveWorldName + ".txt";
            int currentCount = 1;
            while (File.Exists(FinalSavePath)) {
                FinalSavePath = SavePath + SaveWorldName + currentCount + ".txt";
                currentCount++;
            }

            StreamWriter sr = File.CreateText(FinalSavePath);

            for (int i = 0; i < saveObjects.Length; i++) {
                sr.WriteLine(saveObjects[i].ObjectType);
                Transform currentTransform = saveObjects[i].gameObject.transform;
                sr.WriteLine(saveObjects[i].SavePosition ? currentTransform.position : Vector3.zero);
                sr.WriteLine(saveObjects[i].SaveRotation ? currentTransform.rotation : Vector3.zero);
                sr.WriteLine(saveObjects[i].SaveScale ? currentTransform.localScale : Vector3.zero);
            }
            sr.Close();

            Debug.Log("World saved at: " + FinalSavePath);
        });
    }
}
