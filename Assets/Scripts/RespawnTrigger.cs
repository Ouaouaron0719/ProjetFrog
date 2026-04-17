using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    [SerializeField] private BreakableWall[] wallsToRespawn;
    [SerializeField] private bool triggerOnce = false;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null)
            return;

        if (triggerOnce && hasTriggered)
            return;

        foreach (BreakableWall wall in wallsToRespawn)
        {
            if (wall != null)
                wall.RespawnWall();
        }

        hasTriggered = true;
    }
}