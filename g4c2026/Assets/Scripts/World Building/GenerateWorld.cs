using UnityEngine;
using System;
using System.Collections.Generic;

public class GenerateWorld : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextAsset WorldFile;
    public List<GameObject> ObjectPrefabs;
    // size of a chunk
    public static int SquareSize = 2;

    public static int MaxX;
    public static int MaxZ;

    /*public int maxX;
    public int maxZ;*/
    public int squareSize;
    public int renderDistance;
    public static int MaxXChunk;
    public static int MaxZChunk;

    List<List<GenerateObjectData>> ChunkInformation;
    List<List<GameObject>> ChunkGameObjects;

    public struct GenerateObjectData {
        public ObjectType type;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;

        // 2 3
        // 0 1
        public int GetQuadrant() {
            return GenerateWorld.GetChunkNumberFromPosition(position.x, position.z);
        }
    }

    void Start() {
        /*MaxX = maxX;
        MaxZ = maxZ;*/
        SquareSize = squareSize;
        if (WorldFile == null) Debug.LogError("World file not assigned! Cannot load world.");

        string fileContents = WorldFile.text;
        string[] lines = fileContents.Split('\n');

        MaxX = int.Parse(lines[0]);
        MaxZ = int.Parse(lines[1]);
        MaxXChunk = (MaxX + SquareSize) / SquareSize * 2;
        MaxZChunk = (MaxZ + SquareSize) / SquareSize * 2;

        // initialize list

        ChunkInformation = new List<List<GenerateObjectData>>();
        ChunkGameObjects = new List<List<GameObject>>();
        for (int i = 0; i < MaxXChunk * MaxZChunk; i++) {
            ChunkInformation.Add(new List<GenerateObjectData>());
            ChunkGameObjects.Add(new List<GameObject>());
        }

        for (int i = 2; i < lines.Length; i += 5) {
            if (lines[i] == "") continue;
            ObjectType type = (ObjectType)System.Enum.Parse(typeof(ObjectType), lines[i]);
            Vector3 position = StringUtil.StringToVector3(lines[i + 1]);
            Quaternion rotation = StringUtil.StringToQuaternion(lines[i + 2]);
            Vector3 scale = StringUtil.StringToVector3(lines[i + 3]);
            // not spawn on command
            lines[i + 4] = lines[i + 4].Trim();
            if (lines[i + 4] != "True") {
                GameObject obj = Instantiate(ObjectPrefabs[(int)type], position, rotation);
                obj.transform.localScale = scale;
            }
            else {
                GenerateObjectData tmp = new GenerateObjectData() {
                    type = type,
                    position = position,
                    rotation = rotation,
                    scale = scale
                };
                ChunkInformation[tmp.GetQuadrant()].Add(tmp);
            }
        }

        for (int i = GetXCoord(0) - renderDistance; i <= GetXCoord(0) + renderDistance; i++) {
            for (int j = GetZCoord(0) - renderDistance; j <= GetZCoord(0) + renderDistance; j++) {
                SpawnChunk(GetChunkNumberFromIndex(i, j));
            }
        }
    }

    // Update is called once per frame
    void Update() {
        int idx = GetChunkNumberFromPosition(PlayerData.PlayerPosition.x, PlayerData.PlayerPosition.z);
        int startX = GetXCoord(PlayerData.PlayerPosition.x) - renderDistance, endX = GetXCoord(PlayerData.PlayerPosition.x) + renderDistance, startZ = GetZCoord(PlayerData.PlayerPosition.z) - renderDistance, endZ = GetZCoord(PlayerData.PlayerPosition.z) + renderDistance;

        // bottom row
        if (startZ > 0) {
            for (int i = startX; i <= endX; i++) {
                if (i >= MaxXChunk || i < 0) continue;
                SpawnChunk(GetChunkNumberFromIndex(i, startZ));
            }
            if (startZ - 1 > 0) {
                for (int i = startX; i <= endX; i++) {
                    if (i >= MaxXChunk || i < 0) continue;
                    DespawnChunk(GetChunkNumberFromIndex(i, startZ - 1));
                }
            }
        }

        // top row
        if (endZ < MaxZChunk) {
            for (int i = startX; i <= endX; i++) {
                if (i >= MaxXChunk || i < 0) continue;
                SpawnChunk(GetChunkNumberFromIndex(i, endZ));
            }
            if (endZ + 1 < MaxZChunk) {
                for (int i = startX; i <= endX; i++) {
                    if (i >= MaxXChunk || i < 0) continue;
                    DespawnChunk(GetChunkNumberFromIndex(i, endZ + 1));
                }
            }
        }
        
        // left col
        if (startX > 0) {
            for (int i = startZ; i <= endZ; i++) {
                if (i >= MaxZChunk || i < 0) continue;
                SpawnChunk(GetChunkNumberFromIndex(startX, i));
            }
            if (startX - 1 > 0) {
                for (int i = startZ; i <= endZ; i++) {
                    if (i >= MaxZChunk || i < 0) continue;
                    DespawnChunk(GetChunkNumberFromIndex(startX - 1, i));
                }
            }
        }
        // right col
        if (endX < MaxXChunk) {
            for (int i = startZ; i <= endZ; i++) {
                if (i >= MaxZChunk || i < 0) continue;
                SpawnChunk(GetChunkNumberFromIndex(endX, i));
            }
            if (endX + 1 < MaxXChunk) {
                for (int i = startZ; i <= endZ; i++) {
                    if (i >= MaxZChunk || i < 0) continue;
                    DespawnChunk(GetChunkNumberFromIndex(endX + 1, i));
                }
            }
        }
        

        
        /*for (int i = 0; i < ChunkInformation.Count; i++) {
            if (i == idx) continue;
            DespawnChunk(i);
        }
        SpawnChunk(idx);*/
    }

    void SpawnChunk(int chunkNum) {
        if (ChunkGameObjects[chunkNum].Count != 0) return;
        for (int i = 0; i < ChunkInformation[chunkNum].Count; i++) {
            var type = ChunkInformation[chunkNum][i].type;
            var position = ChunkInformation[chunkNum][i].position;
            var rotation = ChunkInformation[chunkNum][i].rotation;
            var scale = ChunkInformation[chunkNum][i].scale;
            GameObject obj = Instantiate(ObjectPrefabs[(int)type], position, rotation);
            obj.transform.localScale = scale;
            ChunkGameObjects[chunkNum].Add(obj);
        }
    }

    void DespawnChunk(int chunkNum) {
        for (int i = 0; i < ChunkGameObjects[chunkNum].Count; i++) Destroy(ChunkGameObjects[chunkNum][i]);
        ChunkGameObjects[chunkNum] = new List<GameObject>();
    }

    public static int GetChunkNumberFromPosition(float x, float z) {
        return GetZCoord(z) * MaxXChunk + GetXCoord(x);
    }
    
    public static int GetChunkNumberFromIndex(int x, int z) {
        return z * MaxXChunk + x;
    }

    public static int GetXCoord(float x) {
        return ((int)x + MaxXChunk / 2 * SquareSize) / SquareSize;
    }

    public static int GetZCoord(float z) {
        return ((int)z + MaxZChunk / 2 * SquareSize) / SquareSize;
    }

}

