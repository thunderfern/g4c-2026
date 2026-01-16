using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 0.5f;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.None;
    }

    
    void Update() {
        ApplyRotation();
        ApplyMovement();
        ApplyResistance();
    }

    void ApplyRotation() {
        transform.rotation = Quaternion.Euler(transform.rotation.x, Input.mousePositionDelta.x + transform.rotation.eulerAngles.y, transform.rotation.z);
    }

    void ApplyMovement() {

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
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.linearVelocity += new Vector3(0, 3f, 0);
        }
        rb.linearVelocity += new Vector3(0, -6f * deltaTime, 0);
        rb.linearVelocity = new Vector3(Mathf.Min(rb.linearVelocity.x, speed), Mathf.Max(rb.linearVelocity.y, -9.81f), Mathf.Min(rb.linearVelocity.z, speed));
    }

    void ApplyResistance() {
        // Gravity

        // Air Resistence
    }
}
