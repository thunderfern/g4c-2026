using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;
using System;

public class Photobook : MonoBehaviour {

    public int width = 2550; 
    public int height = 3300;

    private int currentPage;
    public GameObject myObject;
    private bool gotImage = false;

    // Photobook pages
    private List<List<ThreatSubSection>> PhotobookPages = new List<List<ThreatSubSection>> {
        new List<ThreatSubSection>{ThreatSubSection.Forest1A, ThreatSubSection.Forest1B},
    };

    // Images Store
    private Dictionary<ThreatSubSection, Texture2D> ImageCache = new Dictionary<ThreatSubSection, Texture2D>();

    void Start() {
        // loading all images
        /*var values = Enum.GetValues(typeof(ThreatSubSection)).Cast<ThreatSubSection>().ToArray();
        foreach (var v in values) {
            ImageCache[v] = GetImage(v);
        }*/
    }
    void Update() {
        if (GameManager.I().CurrentGameState == GameState.Photobook && !gotImage) {
            GetCurrentPage();
            gotImage = true;
        }
    }

    private Texture2D GetImage(ThreatSubSection threatSubSection) {
        byte[] bytes = File.ReadAllBytes("Screenshots/Forest1/Forest1A/screenshot0.png");
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false) {
            filterMode = FilterMode.Trilinear
        };
        texture.LoadImage(bytes);
        return texture;
    }

    public void GetCurrentPage() {
        //Sprite sprite = Sprite.Create(GetImage(), new Rect(0,0,width, height), new Vector2(0.5f,0.0f), 1.0f);

        //myObject.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
    }

}