using UnityEngine;
using System.Collections.Generic;

public class Page : MonoBehaviour {
    
    public List<GameObject> twoPageObjects;
    public List<GameObject> twoPageArrows;
    public List<GameObject> sixPageObjects;
    public List<GameObject> sixPageArrows;

    void Start() {

    }

    public void UpdatePage(List<Sprite> sprites, bool transition, bool twoPage) {
        ResetPages();
        if (twoPage) {
            for (int i = 0; i < sprites.Count; i += 2) {
                twoPageObjects[i].GetComponent<UnityEngine.UI.Image>().sprite = sprites[i];
                twoPageObjects[i].SetActive(true);
                if (i + 1 < sprites.Count) {
                    twoPageObjects[i + 1].GetComponent<UnityEngine.UI.Image>().sprite = sprites[i + 1];
                    twoPageObjects[i + 1].SetActive(true);
                }
                if (transition) twoPageArrows[i / 2].SetActive(true);
            }
        }
        else {
            for (int i = 0; i < sprites.Count; i += 2) {
                sixPageObjects[i].GetComponent<UnityEngine.UI.Image>().sprite = sprites[i];
                sixPageObjects[i].SetActive(true);
                if (i + 1 < sprites.Count) {
                    sixPageObjects[i + 1].GetComponent<UnityEngine.UI.Image>().sprite = sprites[i + 1];
                    sixPageObjects[i + 1].SetActive(true);
                }
                if (transition) sixPageArrows[i / 2].SetActive(true);
            }
        }
    }
    
    void ResetPages() {
        for (int i = 0; i < twoPageObjects.Count; i++) twoPageObjects[i].SetActive(false);
        for (int i = 0; i < twoPageArrows.Count; i++) twoPageArrows[i].SetActive(false);
        for (int i = 0; i < sixPageObjects.Count; i++) sixPageObjects[i].SetActive(false);
        for (int i = 0; i < sixPageArrows.Count; i++) sixPageArrows[i].SetActive(false);
    }
}
