using UnityEngine;
using System;

public class EnvironmentManager : MonoBehaviour {

    private static EnvironmentManager _instance;

    private EnvironmentManager() {
        _instance = this;
    }

    public static EnvironmentManager I() {
        if (_instance == null) {
            EnvironmentManager instance = new EnvironmentManager();
            _instance = instance;
        }
        return _instance;
    }

    public GameObject SkyDome;
    public GameObject RainObj;
    public Light LightObj;
    private Material skydomeMat;

    public float DayLength = 30f;
    public float LightMin = 0.05f;
    public float LightMax = 0.5f;
    public float RainLength = 15f;

    public float ElapsedTime;

    void Start() {
        skydomeMat = SkyDome.GetComponent<Renderer>().material;
    }

    void Update() {
        float deltaTime = Time.deltaTime;
        ElapsedTime += deltaTime;
        
        // setting skybox
        // sky box is noon at whole numbers and midnight at .5s
        float skyboxUV = ElapsedTime / DayLength;
        skyboxUV -= (int)skyboxUV;
        skydomeMat.SetTextureOffset("_MainTex", new Vector3(skyboxUV, 0));

        // setting light, range is from 0.2 to 0.5
        float lightLerp = Mathf.Abs(skyboxUV - LightMax) * 2;
        LightObj.intensity = Mathf.Lerp(LightMin, LightMax, lightLerp);

        // setting rain
        if (ElapsedTime % RainLength >= RainLength / 2) RainObj.SetActive(true);
        else RainObj.SetActive(false);

        RainObj.transform.position = PlayerData.PlayerPosition + new Vector3(0, 15.0f, 0);

    }
}