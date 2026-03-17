using UnityEngine;
using System.Collections.Generic;

public class Page : MonoBehaviour {
    
    public List<GameObject> twoPageObjects;
    public List<GameObject> twoPageArrows;
    public List<GameObject> sixPageObjects;
    public List<GameObject> sixPageArrows;

    void Start() {

    }

    public void UpdatePage(List<Texture2D> textures, bool transition, bool twoPage) {
        ResetPages();
        if (twoPage) {
            for (int i = 0; i < textures.Count; i += 2) {
                twoPageObjects[i].GetComponent<UnityEngine.UI.Image>().sprite = GetSprite(textures[i]);
                twoPageObjects[i].SetActive(true);
                if (i + 1 < textures.Count) {
                    twoPageObjects[i + 1].GetComponent<UnityEngine.UI.Image>().sprite = GetSprite(textures[i + 1]);
                    twoPageObjects[i + 1].SetActive(true);
                }
                if (transition) twoPageArrows[i / 2].SetActive(true);
            }
        }
        else {
            for (int i = 0; i < textures.Count; i += 2) {
                sixPageObjects[i].GetComponent<UnityEngine.UI.Image>().sprite = GetSprite(textures[i]);
                sixPageObjects[i].SetActive(true);
                if (i + 1 < textures.Count) {
                    sixPageObjects[i + 1].GetComponent<UnityEngine.UI.Image>().sprite = GetSprite(textures[i + 1]);
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

    Sprite GetSprite(Texture2D texture) {
        Debug.Log(texture.width);
        Sprite sprite = Sprite.Create(texture, new Rect(0,0,texture.width, texture.height), new Vector2(0.5f,0.5f), 1.0f);
        return sprite;
        //myObject.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
    }
}
