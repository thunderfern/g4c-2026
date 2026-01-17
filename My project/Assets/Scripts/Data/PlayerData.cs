using UnityEngine;

public enum PlayerAnimal {
    FOX,
    EAGLE,
    FISH
};

public class PlayerData : MonoBehaviour {

    public PlayerAnimal playerAnimal;

    private Renderer renderer1;
    void Start() {
        playerAnimal = PlayerAnimal.FOX;
        renderer1 = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            playerAnimal = PlayerAnimal.FOX;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            playerAnimal = PlayerAnimal.EAGLE;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            playerAnimal = PlayerAnimal.FISH;
        }

        if (playerAnimal == PlayerAnimal.FOX) renderer1.material.color = new Color(1, 0.64f, 0);
        else if (playerAnimal == PlayerAnimal.EAGLE) renderer1.material.color = new Color(0.62f, 0.16f, 0.40f);
        else renderer1.material.color = new Color(0.53f, 0.81f, 0.94f);
    }
}
