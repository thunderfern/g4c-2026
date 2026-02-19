using UnityEngine;

public enum MovementDirection {
    FORWARD,
    RIGHT,
    LEFT,
    BACKWARD
};

public static class BaseAction {

    public static Vector3 ApplyMovement(Transform transform, Rigidbody rb, MovementDirection movementDirection, float speed, Vector3 oldMovement) {

        float deltaTime = Time.fixedDeltaTime;
        
        float yAngleRadians = transform.rotation.eulerAngles.y * Mathf.PI / 180;
        float zAngleRadians = transform.rotation.eulerAngles.z * Mathf.PI / 180;
        // this is 2d found out
        Vector3 forward = new Vector3(Mathf.Cos(yAngleRadians), 0, -Mathf.Sin(Mathf.PI - yAngleRadians));
        forward += new Vector3(0f, Mathf.Tan(zAngleRadians) * Vector3.Magnitude(forward), 0f);
        // add in third component
        forward = Vector3.Normalize(forward);

        Vector3 left = new Vector3(-Mathf.Sin(Mathf.PI + yAngleRadians), 0f, Mathf.Cos(yAngleRadians));
        
        float currentSpeed = Vector3.Magnitude(oldMovement);

        if (movementDirection == MovementDirection.FORWARD) rb.linearVelocity = forward;
        else if (movementDirection == MovementDirection.BACKWARD) rb.linearVelocity = -forward;
        else if (movementDirection == MovementDirection.RIGHT) rb.linearVelocity = -left;
        else if (movementDirection == MovementDirection.LEFT) rb.linearVelocity = left;

        rb.linearVelocity = rb.linearVelocity * Mathf.Min(currentSpeed + speed * deltaTime, speed);

        return rb.linearVelocity;
    }

    public static float ApplyGravity(Rigidbody rb, float oldGravity, float gravity, float terminalVelocity) {

        float deltaTime = Time.fixedDeltaTime;

        float newGravity = Mathf.Max(oldGravity + gravity * deltaTime, terminalVelocity);

        rb.linearVelocity += new Vector3(0, newGravity, 0);

        return newGravity;
    }

    public static void ApplyResistence(Rigidbody rb, float maxSpeed, float gravity, float resistence) {

        float deltaTime = Time.deltaTime;

        // gravity
        rb.linearVelocity += new Vector3(0, gravity * deltaTime, 0);

        // max speed
        //rb.linearVelocity = new Vector3(Mathf.Min(rb.linearVelocity.x, maxSpeed), Mathf.Max(rb.linearVelocity.y, gravity), Mathf.Min(rb.linearVelocity.z, maxSpeed));

        /*float mag = Vector3.Magnitude(rb.linearVelocity);

        if (rb.linearVelocity.x > 0.0001 || rb.linearVelocity.x < -0.0001) {
            float absx = Mathf.Abs(rb.linearVelocity.x);
            Vector3 xresistence = new Vector3(Mathf.Min(deltaTime * resistence * (absx / mag), absx), 0, 0);
            
            if (rb.linearVelocity.x < 0) rb.linearVelocity += xresistence;
            else rb.linearVelocity -= xresistence;
        }
        else rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, rb.linearVelocity.z);

        if (rb.linearVelocity.z > 0.0001 || rb.linearVelocity.z < -0.0001) {
            float absz = Mathf.Abs(rb.linearVelocity.z);
            Vector3 zresistence = new Vector3(0, 0, Mathf.Min(deltaTime * resistence * (absz / mag), absz));
            
            if (rb.linearVelocity.z < 0) rb.linearVelocity += zresistence;
            else rb.linearVelocity -= zresistence;
        }
        else rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, 0);*/
    }
    public static void ApplyRotationHorizontal(Transform transform, Vector3 mouseDelta) {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, mouseDelta.x / 5 + transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    public static void ApplyRotationVertical(Transform transform, Vector3 mouseDelta) {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + mouseDelta.y);
    }
}