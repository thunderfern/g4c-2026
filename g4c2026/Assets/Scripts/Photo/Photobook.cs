using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections.Generic;
using System;

public class Photobook : MonoBehaviour {

    // Singleton

    private static Photobook _instance;

    private Photobook() {
        _instance = this;
    }

    public static Photobook I() {
        if (_instance == null) {
            Photobook instance = new Photobook();
            _instance = instance;
        }
        return _instance;
    }

    public int width = 512; 
    public int height = 512;

    private int currentPage = 0;

    // Photobook pages
    private List<List<ThreatSubSection>> PhotobookPages = new List<List<ThreatSubSection>> {
        new List<ThreatSubSection>{ThreatSubSection.Forest1A, ThreatSubSection.Forest1B},
        new List<ThreatSubSection>{ThreatSubSection.Forest1A, ThreatSubSection.Forest1B},
        new List<ThreatSubSection>{ThreatSubSection.Forest1HabitatA, ThreatSubSection.Forest1B},
    };

    // Images Store
    public Dictionary<ThreatSubSection, Texture2D> ImageCache = new Dictionary<ThreatSubSection, Texture2D>();

    // UI

    public Button BackButton;
    public Button ForwardButton;
    public Button ExitButton;

    public GameObject PhotobookObject;
    public GameObject LeftPage;
    public GameObject RightPage;
    
    public Sprite UndiscoveredImageSprite;

    void Start() {
        GetCurrentPage();
        BackButton.onClick.AddListener(() => {
            currentPage = Math.Max(currentPage - 1, 0);
            GetCurrentPage();
        });
        ForwardButton.onClick.AddListener(() => {
            currentPage = Math.Min(currentPage + 1, PhotobookPages.Count / 2);
            GetCurrentPage();
        });
        ExitButton.onClick.AddListener(() => {
            GameManager.I().CurrentGameState = GameState.Movement;
        });

        // loading all images
        /*var values = Enum.GetValues(typeof(ThreatSubSection)).Cast<ThreatSubSection>().ToArray();
        foreach (var v in values) {
            ImageCache[v] = GetImage(v);
        }*/
    }
    void Update() {
        if (GameManager.I().CurrentGameState == GameState.Photobook && !PhotobookObject.activeInHierarchy) {
            PhotobookObject.SetActive(true);
        }
        else if (GameManager.I().CurrentGameState != GameState.Photobook) {
            PhotobookObject.SetActive(false);
        }
    }

    private Sprite GetSpriteFromThreatSubSection(ThreatSubSection threatSubSection) {
        /*byte[] bytes = File.ReadAllBytes("Screenshots/Forest1/Forest1HabitatA/Screenshot29.png");
        Texture2D texture = new Texture2D(4000, 4000, TextureFormat.RGB24, false) {
            filterMode = FilterMode.Trilinear
        };
        texture.LoadImage(bytes);
        return texture;*/
        // /Photobook.I().ImageCache[photoCandidate.ThreatSubSection] = screenShot;
        if (!ImageCache.TryGetValue(threatSubSection, out var tmp)) return UndiscoveredImageSprite;

        return GetSpriteFromTexture(tmp);
    }

    public void GetCurrentPage() {
        List<Sprite> leftSprites = new List<Sprite>();
        for (int i = 0; i < PhotobookPages[currentPage].Count; i++) {
            leftSprites.Add(GetSpriteFromThreatSubSection(PhotobookPages[currentPage * 2][i]));
        }
        LeftPage.GetComponent<Page>().UpdatePage(leftSprites, true, currentPage == 0);
        // two transition
        // six transition
        // six blank
    }

    Sprite GetSpriteFromTexture(Texture2D texture) {
        Sprite sprite = Sprite.Create(texture, new Rect(0,0,texture.width, texture.height), new Vector2(0.5f,0.5f), 1.0f);
        return sprite;
        //myObject.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
    }

}