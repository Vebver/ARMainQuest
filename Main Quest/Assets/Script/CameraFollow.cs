using UnityEngine;

public class CameraFollowFixedX : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float smoothSpeed = 0.125f;

    public float fixedXRotation = 31.508f; // Lock X rotation

    void LateUpdate()
    {
        if (target != null)
        {
            // Follow position smoothly
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Keep current Y/Z rotation, but lock X rotation
            Vector3 currentRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(fixedXRotation, currentRotation.y, currentRotation.z);
        }
    }
}
