using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;

public class Page : MonoBehaviour {

    public List<GameObject> PageStyles;

    // for the inspector
    [Serializable]
    public struct PageStructs {
        public TMP_Text PageName;
        public List<Image> Images;
        public List<TMP_Text> Captions;
    }

    public List<PageStructs> PageLayouts;

    void Start() {

    }

    public void UpdatePage(int page, List<Sprite> sprites, List<string> captions, string pageName = "") {
        ResetPages();
        for (int i = 0; i < sprites.Count; i++) {
            PageLayouts[page].Images[i].sprite = sprites[i];
            PageLayouts[page].Captions[i].text = captions[i];
        }
        if (PageLayouts[page].PageName) PageLayouts[page].PageName.text = pageName;
        PageStyles[page].SetActive(true);
    }
    
    void ResetPages() {
        for (int i = 0; i < PageStyles.Count; i++) PageStyles[i].SetActive(false);
    }
}
