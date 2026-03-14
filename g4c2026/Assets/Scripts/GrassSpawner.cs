using UnityEngine;
using System.Collections.Generic;

public class GrassSpawner : MonoBehaviour
{
    public List<GameObject> grassType;
    Vector3 scale;
    Vector3 pos;

    [SerializeField] int density;
    float grassx, grassz;
    int grassnum;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        scale = transform.localScale;
        pos = transform.position;
        for(int i = 0; i < density; i++)
        {
            grassnum = Random.Range(0, grassType.Count);
            grassx = Random.Range((float)pos.x - (float)(scale.x*5), (float)pos.x + (float)(scale.x*5));
            grassz = Random.Range((float)pos.z - (float)(scale.z*5), (float)pos.z + (float)(scale.z*5));
            GameObject grass = Instantiate(grassType[grassnum], new Vector3(grassx, transform.position.y, grassz), Quaternion.Euler(0, Random.Range(0, 360), 90), transform);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
