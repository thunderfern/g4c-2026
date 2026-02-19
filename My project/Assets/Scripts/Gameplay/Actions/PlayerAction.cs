using UnityEngine;

public class PlayerAction : MonoBehaviour {

    public float FoxSpeed;
    public float FoxSprint;
    public float FoxMaxSpeed;
    public float FoxJump;
    public float FoxGravity;
    public float FoxTerminalVelocity;
    public float FoxResistence;

    public float FishSpeed;
    
    private Rigidbody rb;
    private bool isGrounded;
    private PlayerData playerData;

    private float oldGravity;
    private Vector3 oldMovement;
    private Vector3 oldMouse;
    private Vector3 mouseDelta;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerData = GetComponent<PlayerData>();
        isGrounded = false;
        oldMouse = Input.mousePosition;
    }

    void FixedUpdate() {
        mouseDelta = Input.mousePosition - oldMouse;
        oldMouse = Input.mousePosition;
        ApplyAction();
    }

    void ApplyAction() {
        if (playerData.playerAnimal == PlayerAnimal.FOX) ApplyFoxAction();
        else if (playerData.playerAnimal == PlayerAnimal.EAGLE) ApplyEagleAction();
        else ApplyFishAction();
    }

    void ApplyFoxAction() {

        // Applying movement

        if (Input.GetKey(KeyCode.W)) {
            oldMovement = BaseAction.ApplyMovement(transform, rb, MovementDirection.FORWARD, FoxSpeed, oldMovement);
        }
        else if (Input.GetKey(KeyCode.S)) {
            oldMovement = BaseAction.ApplyMovement(transform, rb, MovementDirection.BACKWARD, FoxSpeed, oldMovement);
        }
        else if (Input.GetKey(KeyCode.D)) {
            oldMovement = BaseAction.ApplyMovement(transform, rb, MovementDirection.RIGHT, FoxSpeed, oldMovement);
        }
        else if (Input.GetKey(KeyCode.A)) {
            oldMovement = BaseAction.ApplyMovement(transform, rb, MovementDirection.LEFT, FoxSpeed, oldMovement);
        }
        else
        {
            rb.linearVelocity = new Vector3();
            oldMovement = new Vector3();
        }

        // checking if isGrounded
        if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit))
        {
            if (hit.distance <= 0.50001) isGrounded = true;
            else isGrounded = false;
        }
        else isGrounded = false;

        // Applying jump
        if (Input.GetKey(KeyCode.Space)) {
            if (isGrounded) {
                isGrounded = false;
                oldGravity = FoxJump;
            }
        }
        if (!isGrounded) oldGravity = BaseAction.ApplyGravity(rb, oldGravity, FoxGravity, FoxTerminalVelocity);
        BaseAction.ApplyRotationHorizontal(transform, mouseDelta);
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
        BaseAction.ApplyRotationHorizontal(transform, mouseDelta);
        BaseAction.ApplyRotationVertical(transform, mouseDelta);
    }


    void ApplyEagleAction()
    {
        
    }



    void ApplyResistance() {
        // Gravity

        // Air Resistence
    }
}
