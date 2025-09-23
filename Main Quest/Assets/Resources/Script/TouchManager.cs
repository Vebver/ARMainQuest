using UnityEngine;

public class TouchManager : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if we touched an NPC
                NPC npc = hit.collider.GetComponent<NPC>();
                if (npc != null)
                {
                    npc.TalkToPlayer();
                }
            }
        }
    }
}
