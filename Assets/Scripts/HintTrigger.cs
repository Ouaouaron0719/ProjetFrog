using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    [TextArea]
    [SerializeField] private string message;

    [SerializeField] private bool triggerOnce = true;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null)
            return;

        if (triggerOnce && hasTriggered)
            return;

        GameEvents.ShowMessage(message);

        hasTriggered = true;
    }
}