using UnityEngine;

public class PlayerAction : MonoBehaviour {

    public float speed = 0.5f;
    public float maxSpeed = 0.5f;
    
    private Rigidbody rb;
    private bool isGrounded;
    private PlayerData playerData;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerData = GetComponent<PlayerData>();
        isGrounded = false;
    }

    
    void Update() {
        ApplyAction();
    }

    

    void ApplyAction() {
        if (playerData.playerAnimal == PlayerAnimal.FOX) ApplyFoxAction();
        else if (playerData.playerAnimal == PlayerAnimal.EAGLE) ApplyEagleAction();
        else ApplyFishAction();
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
            rb.linearVelocity += forward * deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.S)) {
            rb.linearVelocity += -forward * deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.linearVelocity += -left * deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.A)) {
            rb.linearVelocity += left * deltaTime * speed;
        }

        // checking if isGrounded
        if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit))
        {
            if (hit.distance <= 0.50001) isGrounded = true;
            else isGrounded = false;
        }
        else isGrounded = false;
        BaseAction.ApplyRotationHorizontal(transform);
        BaseAction.ApplyRotationVertical(transform);
    }


    void ApplyFoxAction() {
        float deltaTime = Time.deltaTime;

        // Applying jump
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isGrounded) {
                rb.linearVelocity += new Vector3(0, 6f, 0);
                isGrounded = false;
            }
            //rb.linearVelocity += new Vector3(0, 3f, 0); flying
        }

        // checking if isGrounded
        if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit))
        {
            if (hit.distance <= 0.50001) isGrounded = true;
            else isGrounded = false;
        }
        else isGrounded = false;

        if (Input.GetKey(KeyCode.W)) {
            BaseAction.ApplyMovement(transform, rb, MovementDirection.FORWARD, speed);
        }
        if (Input.GetKey(KeyCode.S)) {
            BaseAction.ApplyMovement(transform, rb, MovementDirection.BACKWARD, speed);
        }
        if (Input.GetKey(KeyCode.D)) {
            BaseAction.ApplyMovement(transform, rb, MovementDirection.RIGHT, speed);
        }
        if (Input.GetKey(KeyCode.A)) {
            BaseAction.ApplyMovement(transform, rb, MovementDirection.LEFT, speed);
        }

        BaseAction.ApplyResistence(rb, speed, -9.81f, 10f);

        BaseAction.ApplyRotationHorizontal(transform);
    }

    void ApplyEagleAction()
    {
        
    }



    void ApplyResistance() {
        // Gravity

        // Air Resistence
    }
}
