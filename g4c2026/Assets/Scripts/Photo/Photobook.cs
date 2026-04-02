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
        new List<ThreatSubSection>{ThreatSubSection.Forest1A},
        new List<ThreatSubSection>{ThreatSubSection.Forest1B},
        new List<ThreatSubSection>{ThreatSubSection.Forest1SA, ThreatSubSection.Forest1SHA},
        new List<ThreatSubSection>{ThreatSubSection.Forest1SB, ThreatSubSection.Forest1SHB},
        new List<ThreatSubSection>{ThreatSubSection.Forest1SC, ThreatSubSection.Forest1SHC},
        new List<ThreatSubSection>{ThreatSubSection.Forest2A},
        new List<ThreatSubSection>{ThreatSubSection.Forest2B},
        new List<ThreatSubSection>{ThreatSubSection.Forest2SA, ThreatSubSection.Forest2SHA},
        new List<ThreatSubSection>{ThreatSubSection.Forest2SB, ThreatSubSection.Forest2SHB},
        new List<ThreatSubSection>{ThreatSubSection.Farm1A},
        new List<ThreatSubSection>{ThreatSubSection.Farm1B},
        new List<ThreatSubSection>{ThreatSubSection.Farm1SA, ThreatSubSection.Farm1SHA},
        new List<ThreatSubSection>{ThreatSubSection.Port1A},
        new List<ThreatSubSection>{ThreatSubSection.Port1B},
        new List<ThreatSubSection>{ThreatSubSection.Port1SA, ThreatSubSection.Port1SHA},
        new List<ThreatSubSection>{ThreatSubSection.FishStash},
        new List<ThreatSubSection>{ThreatSubSection.BunnyTrash},
        new List<ThreatSubSection>{ThreatSubSection.BunnyDirt},
        new List<ThreatSubSection>{ThreatSubSection.DuckGroup},
        new List<ThreatSubSection>{ThreatSubSection.FoxApple},
        new List<ThreatSubSection>{ThreatSubSection.BunnyApple},
        new List<ThreatSubSection>{ThreatSubSection.SallyHoard},
        new List<ThreatSubSection>{ThreatSubSection.SammyGreed},
        new List<ThreatSubSection>{ThreatSubSection.DuncansBoat},
        new List<ThreatSubSection>{ThreatSubSection.PetersBoat},
        new List<ThreatSubSection>{ThreatSubSection.TommysLunch},
        new List<ThreatSubSection>{ThreatSubSection.TimTamFox},
        new List<ThreatSubSection>{ThreatSubSection.RockGarden},
    };

    private List<string> PhotobookPageNames = new List<string> {
        "Forest 1",
        "Forest 1",
        "Forest 1",
        "Forest 1",
        "Forest 1",
        "Forest 2",
        "Forest 2",
        "Forest 2",
        "Forest 2",
        "Farm",
        "Farm",
        "Farm",
        "Port",
        "Port",
        "Port",
        "Fish Stash",
        "Easter Egg",
        "Easter Egg",
        "Easter Egg",
        "Easter Egg",
        "Easter Egg",
        "Easter Egg",
        "Easter Egg",
        "Easter Egg",
        "Easter Egg",
        "Easter Egg",
        "Easter Egg",
        "Easter Egg",
    };

    // Images Store
    public Dictionary<ThreatSubSection, Texture2D> ImageCache = new Dictionary<ThreatSubSection, Texture2D>();

    // for the inspector
    [Serializable]
    public struct PhotoTitleStruct {
        public ThreatSubSection ThreatSubSection;
        public string PhotoTitle;
    }

    public List<PhotoTitleStruct> PhotoCaptions;

    // UI
    [Header("UI")]
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
            currentPage = Math.Min(currentPage + 1, (PhotobookPages.Count - 1) / 2);
            GetCurrentPage();
        });
        ExitButton.onClick.AddListener(() => {
            GameManager.I().CurrentGameState = GameState.Movement;
            GameManager.I().PerformedAction(new Goal {
                GoalType = GoalType.Open, 
                Arguments = new List<string>() {
                    "Photobook"
                }
            });
        });
    }
    void Update() {
        if (GameManager.I().CurrentGameState == GameState.Photobook && !PhotobookObject.activeInHierarchy) {
            PhotobookObject.SetActive(true);
            GetCurrentPage();
        }
        else if (GameManager.I().CurrentGameState != GameState.Photobook) {
            PhotobookObject.SetActive(false);
        }
    }

    public void GetCurrentPage() {
        List<Sprite> leftSprites = new List<Sprite>();
        List<string> leftCaptions = new List<string>();
        int leftPageNum = currentPage * 2;
        for (int i = 0; i < PhotobookPages[leftPageNum].Count; i++) {
            ThreatSubSection curThreatSub = PhotobookPages[leftPageNum][i];
            leftSprites.Add(GetSpriteFromThreatSubSection(curThreatSub));
            leftCaptions.Add(GetPhotoCaption(curThreatSub, true));
        }
        LeftPage.GetComponent<Page>().UpdatePage(PhotobookPages[leftPageNum].Count / 2, leftSprites, leftCaptions, PhotobookPageNames[leftPageNum]);

        int rightPageNum = currentPage * 2 + 1;
        if (PhotobookPages.Count <= rightPageNum) {
            RightPage.SetActive(false);
            return;
        }
        RightPage.SetActive(true);

        List<Sprite> rightSprites = new List<Sprite>();
        List<string> rightCaptions = new List<string>();
        for (int i = 0; i < PhotobookPages[rightPageNum].Count; i++) {
            ThreatSubSection curThreatSub = PhotobookPages[rightPageNum][i];
            rightSprites.Add(GetSpriteFromThreatSubSection(curThreatSub));
            rightCaptions.Add(GetPhotoCaption(curThreatSub, true));
        }
        RightPage.GetComponent<Page>().UpdatePage(PhotobookPages[rightPageNum].Count / 2, rightSprites, rightCaptions, PhotobookPageNames[rightPageNum]);
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

    public Sprite GetSpriteFromTexture(Texture2D texture) {
        Sprite sprite = Sprite.Create(texture, new Rect(0,0,texture.width, texture.height), new Vector2(0.5f,0.5f), 1.0f);
        return sprite;
    }

    public string GetPhotoCaption(ThreatSubSection threatSubSection, bool checkFound = false) {
        for (int i = 0; i < PhotoCaptions.Count; i++) {
            if (PhotoCaptions[i].ThreatSubSection == threatSubSection) {
                //if (checkFound && !ImageCache.TryGetValue(threatSubSection, out var tmp)) return "???";
                return PhotoCaptions[i].PhotoTitle;
            }
        }
        return "???";
    }
}