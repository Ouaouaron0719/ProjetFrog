using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] private GameObject breakEffectPrefab;

    private bool isBroken = false;

    public void Break()
    {
        if (isBroken) return;
        isBroken = true;

        if (breakEffectPrefab != null)
            Instantiate(breakEffectPrefab, transform.position, Quaternion.identity);

        CameraShake shake = Camera.main.GetComponent<CameraShake>();
        if (shake != null)
            shake.Shake(0.1f, 0.2f);

        Destroy(gameObject);
    }
}