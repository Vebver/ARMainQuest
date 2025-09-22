using UnityEngine;
using Vuforia;

public class PlayerRigidHandler : DefaultObserverEventHandler
{
    [Header("Player Rigidbody")]
    public Rigidbody playerRb; // Drag Player's Rigidbody here (not Alex)

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        if (playerRb != null)
        {
            playerRb.useGravity = true;    // Enable gravity
            playerRb.isKinematic = false;  // Enable physics
        }
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        if (playerRb != null)
        {
            playerRb.useGravity = false;   // Disable gravity
            playerRb.isKinematic = true;   // Freeze in place
        }
    }
}
