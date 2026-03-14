using UnityEngine;
using System;
using System.Collections.Generic;

public class GenerateWorld : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextAsset WorldFile;
    public List<GameObject> ObjectPrefabs;
    // size of a chunk
    public static int SquareSize = 10;

    public static int MaxX;
    public static int MaxZ;

    public int maxX;
    public int maxZ;
    public int squareSize;
    public int renderDistance;
    public static int MaxXChunk;
    public static int MaxZChunk;


    public struct GenerateObjectData {
        public ObjectType type;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;

        // 2 3
        // 0 1
        public int GetQuadrant() {
            return GetZCoord() * (MaxXChunk * 2) + GetXCoord();
        }

        public int GetZCoord() {
            return ((int)position.z + MaxZChunk * SquareSize) / SquareSize;
        }

        public int GetXCoord() {
            return ((int)position.x + MaxXChunk * SquareSize) / SquareSize;
        }
    }

    void Start() {
        MaxX = maxX;
        MaxZ = maxZ;
        SquareSize = squareSize;

        GenerateObjectData tmp1 = new GenerateObjectData() {
            position = new Vector3(-MaxX, 0, -MaxZ)
            
        };
        GenerateObjectData tmp2 = new GenerateObjectData() {
            position = new Vector3(MaxX, 0, -MaxZ)
        };
        GenerateObjectData tmp3 = new GenerateObjectData() {
            position = new Vector3(-MaxX, 0, MaxZ)
        };
        GenerateObjectData tmp4 = new GenerateObjectData() {
            position = new Vector3(MaxX, 0, MaxZ)
        };
        GenerateObjectData tmp5 = new GenerateObjectData() {
            position = new Vector3(1, 0, MaxZ)
        };
        MaxXChunk = (MaxX + SquareSize) / SquareSize;
        MaxZChunk = (MaxZ + SquareSize) / SquareSize;
        Debug.Log(MaxXChunk);
        Debug.Log(MaxZChunk);
        Debug.Log(tmp1.GetQuadrant());
        Debug.Log(tmp2.GetQuadrant());
        Debug.Log(tmp3.GetQuadrant());
        Debug.Log(tmp4.GetQuadrant());
        Debug.Log(tmp5.GetQuadrant());

        /*if (WorldFile == null) Debug.LogError("World file not assigned! Cannot load world.");

        string fileContents = WorldFile.text;
        string[] lines = fileContents.Split('\n');
        MaxX = int.Parse(lines[0]);
        MaxZ = int.Parse(lines[1]);
        MaxX = (MaxX - 1) / SquareSize * SquareSize;
        MaxZ = (MaxZ - 1) / SquareSize * SquareSize;
        for (int i = 2; i < lines.Length; i += 5) {
            if (lines[i] == "") continue;
            ObjectType type = (ObjectType)System.Enum.Parse(typeof(ObjectType), lines[i]);
            Vector3 position = StringUtil.StringToVector3(lines[i + 1]);
            Quaternion rotation = StringUtil.StringToQuaternion(lines[i + 2]);
            Vector3 scale = StringUtil.StringToVector3(lines[i + 3]);
            // not spawn on command
            if (lines[i + 4] == "0") {
                GameObject obj = Instantiate(ObjectPrefabs[(int)type], position, rotation);
                obj.transform.localScale = scale;
            }
        }*/

    }

    // Update is called once per frame
    void Update() {
        
    }
}

