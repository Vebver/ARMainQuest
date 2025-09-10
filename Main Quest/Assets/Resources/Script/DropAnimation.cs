using UnityEngine;

public class FloatingUI : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float lifetime = 2f;

    void Update()
    {
        // Move upward
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        // Count down lifetime
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) Destroy(gameObject);
    }
}
