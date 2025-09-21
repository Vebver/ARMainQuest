using UnityEngine;
using Vuforia;

public class FollowWithFixedRotation : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // Character to follow

    [Header("Fixed Rotation")]
    public Vector3 fixedRotation = new Vector3(30.707f, 160f, 0f);

    private Vector3 offset; // Difference between camera and player at start
    private bool isTracking = false; // tracking status

    void Start()
    {
        if (target == null) return;

        // Calculate initial offset
        offset = transform.position - target.position;

        // Apply fixed rotation at start
        transform.rotation = Quaternion.Euler(fixedRotation);

        // Subscribe to Vuforia tracking events
        var observer = FindObjectOfType<DefaultObserverEventHandler>();
        if (observer != null)
        {
            observer.OnTargetFound.AddListener(OnTargetFound);
            observer.OnTargetLost.AddListener(OnTargetLost);
        }
    }

    void LateUpdate()
    {
        if (!isTracking || target == null) return;

        // Follow target with fixed offset
        transform.position = target.position + offset;

        // Keep fixed rotation
        transform.rotation = Quaternion.Euler(fixedRotation);
    }

    private void OnTargetFound()
    {
        isTracking = true;
        enabled = true;  // enable script
        Debug.Log("✅ Tracking started, FollowWithFixedRotation enabled.");
    }

    private void OnTargetLost()
    {
        isTracking = false;
        enabled = false; // disable script
        Debug.Log("❌ Tracking lost, FollowWithFixedRotation disabled.");
    }
}
