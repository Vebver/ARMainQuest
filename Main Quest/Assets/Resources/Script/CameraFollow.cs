using UnityEngine;

public class FollowWithFixedRotation : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // Character to follow

    [Header("Fixed Rotation")]
    public Vector3 fixedRotation = new Vector3(30.707f, 160f, 0f);

    private Vector3 offset; // Difference between camera and player at start

    void Start()
    {
        if (target == null) return;

        // Calculate initial offset from player to camera
        offset = transform.position - target.position;

        // Apply fixed rotation at start
        transform.rotation = Quaternion.Euler(fixedRotation);
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Follow player by keeping the same offset
        transform.position = target.position + offset;

        // Keep fixed rotation
        transform.rotation = Quaternion.Euler(fixedRotation);
    }
}