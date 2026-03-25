using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/**public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI startText;
    Color transparency;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(transparencyAnimation());
        transparency = startText.color;
    }

    // Update is called once per frame
    void Update()
    {   
        transparency = startText.color;
        if ((float)transparency.a == 1)
        {
            StartCoroutine(transparencyAnimation());
        }
    }

    IEnumerator transparencyAnimation(){
        while ((float)transparency.a > 0.5)
        {
            transparency.a -= 0.1;
            startText.color = transparency;
            yield return null;
        }
        while ((float)transparency.a < 0.3)
        {
            transparency.a += 0.1;
            startText.color = transparency;
            yield return null;
        }


    }
    
}
**/