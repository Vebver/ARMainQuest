using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("References")]
    public GameObject alex; // Assign Alex (child with Animator) in Inspector

    private Animator animator;
    private Vector3 lastPos;

    void Start()
    {
        if (alex != null)
        {
            animator = alex.GetComponent<Animator>();
        }

        lastPos = transform.position;
    }

    void Update()
    {
        if (animator == null || alex == null) return;

        Vector3 currentPos = transform.position;

        // Check if player is moving
        bool isWalking = Mathf.Abs(currentPos.z - lastPos.z) > 0.01f || Mathf.Abs(currentPos.x - lastPos.x) > 0.01f;
        animator.SetBool("IsWalking", isWalking);

        // Decide Alex's facing direction
        if (currentPos.z > lastPos.z) // Forward
        {
            alex.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (currentPos.z < lastPos.z) // Backward
        {
            alex.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        // 🖱️ Trigger attack on right-click (Mouse1)
        if (Input.GetMouseButtonDown(1)) // 1 = Right Click
        {
            TriggerAttack();
        }

        lastPos = currentPos;
    }

    public void TriggerAttack()
    {
        if (animator != null)
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Attack");
        }
    }
}