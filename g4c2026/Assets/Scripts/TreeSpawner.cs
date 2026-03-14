using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        for (int i = -20; i < 20; i++) {
            for (int j = -20; j < 20; j++) {
                Instantiate(treePrefab, new Vector3(i, 0, j), Quaternion.identity, transform);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
