using UnityEngine;

public class FollowWithFixedRotation : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform player; // Assign in Inspector

    [Header("Fixed Rotation")]
    public Vector3 fixedRotation = new Vector3(30.707f, 160f, 0f);

    private Vector3 offset; // Difference between camera and player at start
    private bool isTargetActive = false;

    void Start()
    {
        if (player != null)
        {
            offset = transform.position - player.position;
            isTargetActive = true;

            // Optionally show the player at start
            player.gameObject.SetActive(true);
        }
    }

    void LateUpdate()
    {
        if (!isTargetActive || player == null) return;

        // Follow player by keeping the same offset
        transform.position = player.position + offset;

        // Keep fixed rotation
        transform.rotation = Quaternion.Euler(fixedRotation);
    }

    // Optional: Call this method to manually hide the player
    public void HideTarget()
    {
        isTargetActive = false;
        if (player != null)
            player.gameObject.SetActive(false);
    }

    // Optional: Call this method to manually show the player
    public void ShowTarget()
    {
        if (player != null)
        {
            isTargetActive = true;
            offset = transform.position - player.position;
            transform.rotation = Quaternion.Euler(fixedRotation);
            player.gameObject.SetActive(true);
        }
    }
}