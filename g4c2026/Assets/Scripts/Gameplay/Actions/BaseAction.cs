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
        Vector3 newDirection = new Vector3(-Mathf.Sin(Mathf.PI - newAngle / 180 * Mathf.PI), 0, -Mathf.Cos(newAngle / 180 * Mathf.PI)).normalized * distance + new Vector3(0, transform.position.y - center.y, 0);
        if (!CheckCameraPos(center, newDirection)) return;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, newAngle, transform.rotation.eulerAngles.z);
        transform.position = center + newDirection;
    }

    public static void ApplyRotationVertical(Transform transform, Vector3 mouseDelta, Vector3 center, float lowBound = 310, float highBound = 70) {
        float newAngle = -mouseDelta.y / 5 + transform.rotation.eulerAngles.x;
        // setting up bounds
        if (newAngle >= highBound && newAngle <= lowBound) {
            if (Math.Abs(newAngle - lowBound) < Math.Abs(newAngle - highBound)) newAngle = lowBound;
            else newAngle = highBound;
        }
        float distance = (transform.position - center).magnitude;

        Vector3 xzDirection = new Vector3(transform.position.x - center.x, 0, transform.position.z - center.z).normalized;
        
        // first is xz
        Vector2 newMag = new Vector2(Mathf.Cos(newAngle / 180 * Mathf.PI), Mathf.Sin(newAngle / 180 * Mathf.PI)).normalized;

        Vector3 newDirection = xzDirection * newMag.x * distance + new Vector3(0, newMag.y, 0) * distance;

        if (!CheckCameraPos(center, newDirection)) return;

        transform.position = center + newDirection;
        transform.rotation = Quaternion.Euler(newAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        
    }

    public static void ApplyCameraZoom(Transform transform, Vector2 mouseScrollDelta, Vector3 center, float lowBound = 10, float highBound = 20) {
        Vector3 direction = transform.position - center;
        float distance = direction.magnitude;
        float newDistance = Math.Min(Math.Max(distance - mouseScrollDelta.y, lowBound), highBound);
        Vector3 newDirection = direction.normalized * newDistance;
        if (!CheckCameraPos(center, newDirection)) return;
        transform.position = center + newDirection;
    }

    public static bool CheckCameraPos(Vector3 center, Vector3 offset) {
        //if (Physics.OverlapSphere(center + offset, 0.1f).Length > 0) return false;
        //if (Physics.Raycast(center, offset, out RaycastHit hit, (center - offset).magnitude, LayerMask.GetMask("Ground"))) return false;
        if (Physics.Raycast(center + offset, -Vector3.up, out RaycastHit hit1, 1f, LayerMask.GetMask("Ground"))) return false;
        return true;
    }
}