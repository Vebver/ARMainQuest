using UnityEngine;

public class CameraFollowFixedX : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float smoothSpeed = 0.125f;

    [Header("Fixed Values")]
    public float fixedXRotation = 31.508f; // Lock X rotation
    public float fixedYRotation = 87.017f;      // Lock Y rotation
    public float fixedYPosition = 0.85f;      // Lock Y position (height)

    void LateUpdate()
    {
        if (target != null)
        {
            // Follow target smoothly (but override Y position later)
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Lock Y position
            smoothedPosition.y = fixedYPosition;
            transform.position = smoothedPosition;

            // Lock X and Y rotation, keep Z as is
            Vector3 currentRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(fixedXRotation, fixedYRotation, currentRotation.z);
        }
    }
}
