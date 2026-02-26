using UnityEngine;
using System.Collections.Generic;

public class GenerateWorld : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextAsset WorldFile;
    public List<GameObject> ObjectPrefabs;

    void Start() {
        if (WorldFile == null) Debug.LogError("World file not assigned! Cannot load world.");

        string fileContents = WorldFile.text;
        string[] lines = fileContents.Split('\n');
        for (int i = 0; i < lines.Length; i += 4) {
            if (lines[i] == "") continue;
            ObjectType currentType = (ObjectType)System.Enum.Parse(typeof(ObjectType), lines[i]);
            Vector3 currentPosition = StringUtil.StringToVector3(lines[i + 1]);
            Quaternion currentRotation = StringUtil.StringToQuaternion(lines[i + 2]);
            Vector3 currentScale = StringUtil.StringToVector3(lines[i + 3]);
            GameObject obj = Instantiate(ObjectPrefabs[(int)currentType], currentPosition, currentRotation);
            obj.transform.localScale = currentScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

