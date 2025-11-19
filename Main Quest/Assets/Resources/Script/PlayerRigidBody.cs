using UnityEngine;

public class PlayerRigidHandler : MonoBehaviour
{
    [Header("Player Rigidbody")]
    public Rigidbody playerRb; // Assign in Inspector

    private void Start()
    {
        // Optional: Set initial state
        if (playerRb != null)
        {
            playerRb.useGravity = false;
            playerRb.isKinematic = true;
        }
    }

    // Call this when you want to "activate" the player
    public void EnablePhysics()
    {
        if (playerRb != null)
        {
            playerRb.useGravity = true;
            playerRb.isKinematic = false;
        }
    }

    // Call this when you want to "deactivate" the player
    public void DisablePhysics()
    {
        if (playerRb != null)
        {
            playerRb.useGravity = false;
            playerRb.isKinematic = true;
        }
    }
}