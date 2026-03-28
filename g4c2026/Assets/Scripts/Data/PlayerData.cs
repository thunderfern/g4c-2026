using UnityEngine;

public enum PlayerAnimal {
    Human,
    Fox,
    Rabbit,
};

public class PlayerData : MonoBehaviour {

    public PlayerAnimal playerAnimal;

    public static Vector3 PlayerPosition;
    public static Item PlayerInventory;

    private Renderer renderer1;
    void Start() {
        playerAnimal = PlayerAnimal.Human;
        //renderer1 = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update() {
        PlayerPosition = transform.position;
    }
}
