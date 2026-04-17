using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    [SerializeField] private bool triggerOnce = true;
    [SerializeField] private bool resetAbilities = false;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player == null)
            return;

        if (triggerOnce && hasTriggered)
            return;

        hasTriggered = true;

        if (resetAbilities && DatabaseManager.Instance != null)
        {
            DatabaseManager.Instance.ResetAllAbilities();
        }

        SceneManager.LoadScene(targetSceneName);
    }
}