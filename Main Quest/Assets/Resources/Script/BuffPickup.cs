using UnityEngine;

public class BuffPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("✅ Triggered by: " + other.name);
    }
}
