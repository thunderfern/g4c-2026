using UnityEngine;

public enum PlayerAnimal {
    Fox,
    Eagle,
    Fish
};

public class PlayerData : MonoBehaviour {

    public PlayerAnimal playerAnimal;

    public static Vector3 PlayerPosition;
    public static Item PlayerInventory;

    private Renderer renderer1;
    void Start() {
        playerAnimal = PlayerAnimal.Fox;
        //renderer1 = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update() {
        PlayerPosition = transform.position;
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            playerAnimal = PlayerAnimal.Fox;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            playerAnimal = PlayerAnimal.Eagle;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            playerAnimal = PlayerAnimal.Fish;
        }

        /*if (playerAnimal == PlayerAnimal.Fox) renderer1.material.color = new Color(1, 0.64f, 0);
        else if (playerAnimal == PlayerAnimal.Eagle) renderer1.material.color = new Color(0.62f, 0.16f, 0.40f);
        else renderer1.material.color = new Color(0.53f, 0.81f, 0.94f);*/
    }
}
