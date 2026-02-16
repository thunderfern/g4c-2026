using UnityEngine;

public enum ObjectType {
    TREE1 = 0,
    TREE2,
    TREETRUNK
}

public class SaveObject : MonoBehaviour {
    public bool SavePosition = true;
    public bool SaveRotation = true;
    public bool SaveScale = true;
    public ObjectType ObjectType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
}
