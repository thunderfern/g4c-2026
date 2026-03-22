using UnityEngine;
using System;

public enum MovementDirection {
    FORWARD,
    RIGHT,
    LEFT,
    BACKWARD
};

public static class BaseAction {

    public static void ApplyMovement(Transform animalTransform, Rigidbody rb, Vector3 forward, Vector3 direction, float speed) {

        float deltaTime = Time.fixedDeltaTime;

        forward = new Vector3(forward.x, 0, forward.z);

        forward.Normalize();
        direction.Normalize();
        Vector3 right = Vector3.Cross(new Vector3(0, 1, 0), forward);
        
        Vector3 realDirection = (forward * direction.z + right * direction.x).normalized;

        // rotating the animal to face correct direction
        if (realDirection != Vector3.zero) animalTransform.forward = realDirection;

        rb.linearVelocity = realDirection * speed;
    }

    public static float ApplyGravity(Rigidbody rb, float oldGravity, float gravity) {

        float deltaTime = Time.fixedDeltaTime;

        float newGravity = oldGravity + gravity * deltaTime;

        rb.linearVelocity += new Vector3(0, newGravity, 0);

        return newGravity;
    }

    public static void ApplyRotationHorizontal(Transform transform, Vector3 mouseDelta, Vector3 center) {
        float newAngle = mouseDelta.x / 5 + transform.rotation.eulerAngles.y;
        float distance = new Vector3(transform.position.x - center.x, 0, transform.position.z - center.z).magnitude;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, newAngle, transform.rotation.eulerAngles.z);
        transform.position = center + new Vector3(-Mathf.Sin(Mathf.PI - newAngle / 180 * Mathf.PI), 0, -Mathf.Cos(newAngle / 180 * Mathf.PI)).normalized * distance + new Vector3(0, transform.position.y - center.y, 0);
    }

    public static void ApplyRotationVertical(Transform transform, Vector3 mouseDelta, Vector3 center) {
        float newAngle = -mouseDelta.y / 5 + transform.rotation.eulerAngles.x;
        // setting up bounds
        if (newAngle >= 80 && newAngle <= 300) {
            if (Math.Abs(newAngle - 80) < Math.Abs(newAngle - 300)) newAngle = 80;
            else newAngle = 300;
        }
        float distance = (transform.position - center).magnitude;
        transform.rotation = Quaternion.Euler(newAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        
        Vector3 xzDirection = new Vector3(transform.position.x - center.x, 0, transform.position.z - center.z).normalized;
        
        // first is xz
        Vector2 newMag = new Vector2(Mathf.Cos(newAngle / 180 * Mathf.PI), Mathf.Sin(newAngle / 180 * Mathf.PI)).normalized;

        Vector3 newDirection = xzDirection * newMag.x * distance + new Vector3(0, newMag.y, 0) * distance;

        transform.position = center + newDirection;
    }

    public static void ApplyCameraZoom(Transform transform, Vector2 mouseScrollDelta, Vector3 center) {
        Vector3 direction = transform.position - center;
        float distance = direction.magnitude;
        float newDistance = Math.Min(Math.Max(distance - mouseScrollDelta.y, 10f), 20f);
        Vector3 newDirection = direction.normalized * newDistance;
        transform.position = center + newDirection;
    }
}