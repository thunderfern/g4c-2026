using UnityEngine;
using System.Collections.Generic;

public class PlayerAction : MonoBehaviour {

    public GameObject Camera;
    public GameObject AnimalGameObject;
    public List<GameObject> AnimalGameObjects;
    public GameObject Feet;

    [Header("Movement Parameters")]

    public float HumanSpeed;
    public float HumanJump;
    public float HumanGravity;

    public float FoxSpeed;
    public float FoxJump;
    public float FoxGravity;

    public float RabbitSpeed;
    public float RabbitJump;
    public float RabbitGravity;
    
    private Rigidbody rb;
    public bool isGrounded;
    private PlayerData playerData;

    private float oldGravity;

    private float lastY;
    
    void Start() {
        rb = GetComponent<Rigidbody>();
        playerData = GetComponent<PlayerData>();
        isGrounded = false;
        for (int i = 0; i < AnimalGameObjects.Count; i++) AnimalGameObjects[i].SetActive(false);
        AnimalGameObjects[0].SetActive(true);
    }

    void Update() {
        if (GameManager.I().CurrentGameState != GameState.Movement || Input.GetKey(KeyCode.LeftControl)) {
            rb.linearVelocity = Vector3.zero;
            return;
        }
        BaseAction.ApplyRotationHorizontal(Camera.transform, Input.mousePositionDelta, transform.position);
        BaseAction.ApplyRotationVertical(Camera.transform, Input.mousePositionDelta, transform.position);
        BaseAction.ApplyCameraZoom(Camera.transform, Input.mouseScrollDelta, transform.position);
        TransformAnimal();
    }

    void FixedUpdate() {
        if (GameManager.I().CurrentGameState != GameState.Movement) return;
        switch (playerData.playerAnimal) {
            case PlayerAnimal.Human:
                ApplyAction(HumanSpeed, HumanJump, HumanGravity);
                break;
            case PlayerAnimal.Fox:
                ApplyAction(FoxSpeed, FoxJump, FoxGravity);
                break;
            case PlayerAnimal.Rabbit:
                ApplyAction(RabbitSpeed, RabbitJump, RabbitGravity);
                break;
            default:
                break;
        }
    }

    void TransformAnimal() {
        if (Input.GetKeyDown(KeyCode.Alpha1) && isGrounded) {
            playerData.playerAnimal = PlayerAnimal.Human;
            for (int i = 0; i < AnimalGameObjects.Count; i++) AnimalGameObjects[i].SetActive(false);
            AnimalGameObjects[0].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && isGrounded) {
            playerData.playerAnimal = PlayerAnimal.Fox;
            for (int i = 0; i < AnimalGameObjects.Count; i++) AnimalGameObjects[i].SetActive(false);
            AnimalGameObjects[1].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && isGrounded) {
            playerData.playerAnimal = PlayerAnimal.Rabbit;
            for (int i = 0; i < AnimalGameObjects.Count; i++) AnimalGameObjects[i].SetActive(false);
            AnimalGameObjects[2].SetActive(true);
            GameManager.I().PerformedAction(new Goal {
                GoalType = GoalType.Turn, 
                Arguments = new List<string>() {
                    "Rabbit"
                }
            });
        }
    }

    void ApplyAction(float speed, float jump, float gravity) {
        // Applying movement

        Vector3 movementDirection = new Vector3();

        if (Input.GetKey(KeyCode.W)) {
            movementDirection += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S)) {
            movementDirection += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.D)) {
            movementDirection += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.A)) {
            movementDirection += new Vector3(-1, 0, 0);
        }
        BaseAction.ApplyMovement(AnimalGameObject.transform, rb, transform.position - Camera.transform.position, movementDirection, speed);

        // checking if isGrounded
        if (Physics.Raycast(Feet.transform.position, -Vector3.up, out RaycastHit hit, 1f, LayerMask.GetMask("Ground"))) {
            // checking if near ground and is not already going up
            if (hit.distance <= 0.1f && oldGravity <= 0.1f) isGrounded = true;
            else isGrounded = false;
        }
        else isGrounded = false;

        // Applying jump
        if (Input.GetKey(KeyCode.Space)) {
            if (isGrounded) {
                isGrounded = false;
                oldGravity = jump;
            }
        }
        // accounts for if the legs are hanging off
        if (!isGrounded) oldGravity = lastY == transform.position.y && oldGravity < -0.1f ? -0.1f : BaseAction.ApplyGravity(rb, oldGravity, gravity);

        lastY = transform.position.y;
    }
}
