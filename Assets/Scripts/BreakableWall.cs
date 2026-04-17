using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] private GameObject breakEffectPrefab;

    private bool isBroken = false;
    private Collider2D wallCollider;
    private SpriteRenderer wallRenderer;

    private void Awake()
    {
        wallCollider = GetComponent<Collider2D>();
        wallRenderer = GetComponent<SpriteRenderer>();
    }

    public void Break()
    {
        if (isBroken) return;
        isBroken = true;

        if (breakEffectPrefab != null)
            Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);

        CameraShake shake = Camera.main.GetComponent<CameraShake>();
        if (shake != null)
            shake.Shake(0.1f, 0.2f);

        if (wallCollider != null)
            wallCollider.enabled = false;

        if (wallRenderer != null)
            wallRenderer.enabled = false;
    }

    public void RespawnWall()
    {
        isBroken = false;

        if (wallCollider != null)
            wallCollider.enabled = true;

        if (wallRenderer != null)
            wallRenderer.enabled = true;
    }
}