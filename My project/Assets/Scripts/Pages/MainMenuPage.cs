using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuPage : MonoBehaviour {
    public Button PlayButton;

    void Start() {
        PlayButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Lin Xin");
        });
    }
}
