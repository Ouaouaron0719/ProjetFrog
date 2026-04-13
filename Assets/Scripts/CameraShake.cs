using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Vector3 baseLocalPosition;
    private Coroutine shakeCoroutine;

    private void Awake()
    {
        baseLocalPosition = transform.localPosition;
    }

    public void Shake(float duration, float magnitude)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            transform.localPosition = baseLocalPosition;
        }

        shakeCoroutine = StartCoroutine(DoShake(duration, magnitude));
    }

    private IEnumerator DoShake(float duration, float magnitude)
    {
        float timer = 0f;

        while (timer < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = baseLocalPosition + new Vector3(offsetX, offsetY, 0f);

            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.localPosition = baseLocalPosition;
        shakeCoroutine = null;
    }
}