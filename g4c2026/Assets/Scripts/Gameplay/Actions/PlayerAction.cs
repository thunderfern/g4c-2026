using UnityEngine;
using System.Collections.Generic;

public class PlayerAction : MonoBehaviour {

    public GameObject Camera;
    public GameObject AnimalGameObject;
    public GameObject Feet;

    public float FoxSpeed;
    public float FoxSprint;
    public float FoxJump;
    public float FoxGravity;

    public float FishSpeed;
    
    private Rigidbody rb;
    private bool isGrounded;
    private PlayerData playerData;

    private float oldGravity;
    
    void Start() {
        rb = GetComponent<Rigidbody>();
        playerData = GetComponent<PlayerData>();
        isGrounded = false;
    }

    void Update() {
        if (GameManager.I().CurrentGameState != GameState.Movement) return;
        BaseAction.ApplyRotationHorizontal(Camera.transform, Input.mousePositionDelta, transform.position);
        BaseAction.ApplyRotationVertical(Camera.transform, Input.mousePositionDelta, transform.position);
        BaseAction.ApplyCameraZoom(Camera.transform, Input.mouseScrollDelta, transform.position);
        TransformAnimal();
    }

    void FixedUpdate() {
        if (GameManager.I().CurrentGameState != GameState.Movement) return;
        ApplyAction();
    }

    void TransformAnimal() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            playerData.playerAnimal = PlayerAnimal.Fox;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            playerData.playerAnimal = PlayerAnimal.Rabbit;
            GameManager.I().PerformedAction(new Goal {
                GoalType = GoalType.Turn, 
                Arguments = new List<string>() {
                    "Rabbit"
                }
            });

        }
    }

    void ApplyAction() {
        if (playerData.playerAnimal == PlayerAnimal.Fox) ApplyFoxAction();
        else if (playerData.playerAnimal == PlayerAnimal.Eagle) ApplyEagleAction();
        else ApplyFishAction();
    }

    void ApplyFoxAction() {

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
        BaseAction.ApplyMovement(AnimalGameObject.transform, rb, transform.position - Camera.transform.position, movementDirection, FoxSpeed);

        // checking if isGrounded
        if (Physics.Raycast(Feet.transform.position, -Vector3.up, out RaycastHit hit)) {
            // might need to make actual feet
            if (hit.distance <= 0.1f) isGrounded = true;
            else isGrounded = false;
        }
        else isGrounded = false;

        // Applying jump
        if (Input.GetKey(KeyCode.Space)) {
            if (isGrounded && oldGravity < 0) {
                isGrounded = false;
                oldGravity = FoxJump;
            }
        }
        if (!isGrounded) oldGravity = BaseAction.ApplyGravity(rb, oldGravity, FoxGravity);
    }

    void ApplyFishAction() {
        float deltaTime = Time.deltaTime;

        float yAngleRadians = transform.rotation.eulerAngles.y * Mathf.PI / 180;
        float zAngleRadians = transform.rotation.eulerAngles.z * Mathf.PI / 180;
        // this is 2d found out
        Vector3 forward = new Vector3(Mathf.Cos(yAngleRadians), 0f, -Mathf.Sin(Mathf.PI - yAngleRadians));
        forward += new Vector3(0f, Mathf.Tan(zAngleRadians) * Vector3.Magnitude(forward), 0f);
        forward = Vector3.Normalize(forward);
        Vector3 left = new Vector3(-Mathf.Sin(Mathf.PI + yAngleRadians), 0f, Mathf.Cos(yAngleRadians));
        if (Input.GetKey(KeyCode.W)) {
            rb.linearVelocity += forward * deltaTime * FishSpeed;
        }
        if (Input.GetKey(KeyCode.S)) {
            rb.linearVelocity += -forward * deltaTime * FishSpeed;
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.linearVelocity += -left * deltaTime * FishSpeed;
        }
        if (Input.GetKey(KeyCode.A)) {
            rb.linearVelocity += left * deltaTime * FishSpeed;
        }

        // checking if isGrounded
        if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit))
        {
            if (hit.distance <= 0.50001) isGrounded = true;
            else isGrounded = false;
        }
        else isGrounded = false;
    }


    void ApplyEagleAction()
    {
        
    }



    void ApplyResistance() {
        // Gravity

        // Air Resistence
    }
}
