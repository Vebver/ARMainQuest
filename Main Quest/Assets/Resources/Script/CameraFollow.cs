using UnityEngine;
using Vuforia; // Import Vuforia namespace

public class FollowWithFixedRotation : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform player; // The player object (assign in Inspector)
    private Transform target; // Active target (set when found)

    [Header("Fixed Rotation")]
    public Vector3 fixedRotation = new Vector3(30.707f, 160f, 0f);

    private Vector3 offset; // Difference between camera and player at start

    void Start()
    {
        // Find the Vuforia Observer (Image Target) in the scene
        var observer = FindObjectOfType<DefaultObserverEventHandler>();

        if (observer != null)
        {
            // Subscribe to tracking events
            observer.OnTargetFound.AddListener(SetTargetFound);
            observer.OnTargetLost.AddListener(SetTargetLost);
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Follow player by keeping the same offset
        transform.position = target.position + offset;

        // Keep fixed rotation
        transform.rotation = Quaternion.Euler(fixedRotation);
    }

    // Called when image target is found
    private void SetTargetFound()
    {
        if (player != null)
        {
            target = player;

            // Recalculate offset
            offset = transform.position - target.position;
            transform.rotation = Quaternion.Euler(fixedRotation);

            // Show the player
            player.gameObject.SetActive(true);
        }
    }

    private void SetTargetLost()
    {
        target = null;

        // Hide the player
        if (player != null)
            player.gameObject.SetActive(false);
    }

}
