using UnityEngine;

public class PlayerAction : MonoBehaviour {

    public float speed = 0.5f;
    
    private Rigidbody rb;
    private bool isGrounded;
    private PlayerData playerData;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerData = GetComponent<PlayerData>();
        Cursor.lockState = CursorLockMode.None;
        isGrounded = false;
    }

    
    void Update() {
        ApplyMovement();
    }

    

    void ApplyMovement() {
        if (playerData.playerAnimal == PlayerAnimal.FOX) ApplyFoxMovement();
        //else if (playerData.playerAnimal == PlayerAnimal.EAGLE) ApplyEagleMovement();
        else ApplyFishMovement();
    }

    void ApplyFishMovement() {
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
        ApplyRotationHorizontal();
        ApplyRotationVertical();

        
    }


    void ApplyFoxMovement() {
        float deltaTime = Time.deltaTime;

        float yAngleRadians = transform.rotation.eulerAngles.y * Mathf.PI / 180;
        Vector3 forward = new Vector3(Mathf.Cos(yAngleRadians), 0f, -Mathf.Sin(Mathf.PI - yAngleRadians));
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

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isGrounded) {
                rb.linearVelocity += new Vector3(0, 6f, 0);
                isGrounded = false;
            }
            //rb.linearVelocity += new Vector3(0, 3f, 0); flying
        }
        rb.linearVelocity += new Vector3(0, -6f * deltaTime, 0);
        rb.linearVelocity = new Vector3(Mathf.Min(rb.linearVelocity.x, speed), Mathf.Max(rb.linearVelocity.y, -9.81f), Mathf.Min(rb.linearVelocity.z, speed));
        
        ApplyRotationHorizontal();
    }

    void ApplyRotationHorizontal() {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Input.mousePositionDelta.x + transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    void ApplyRotationVertical() {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + Input.mousePositionDelta.y);
    }

    void ApplyResistance() {
        // Gravity

        // Air Resistence
    }
}
