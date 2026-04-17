using UnityEngine;
using TMPro;
using System.Collections;

public class UIMessageManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Display Time Settings")]
    [SerializeField] private float baseDisplayTime = 1.2f;
    [SerializeField] private float timePerCharacter = 0.05f;
    [SerializeField] private float maxDisplayTime = 4f;

    private Coroutine currentCoroutine;

    private void Awake()
    {
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameEvents.OnShowMessage += ShowMessage;
    }

    private void OnDisable()
    {
        GameEvents.OnShowMessage -= ShowMessage;
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);

        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        float displayTime = CalculateDisplayTime(message);
        currentCoroutine = StartCoroutine(HideAfterTime(displayTime));
    }

    private float CalculateDisplayTime(string message)
    {
        if (string.IsNullOrEmpty(message))
            return baseDisplayTime;

        float calculatedTime = baseDisplayTime + message.Length * timePerCharacter;
        return Mathf.Min(calculatedTime, maxDisplayTime);
    }

    private IEnumerator HideAfterTime(float displayTime)
    {
        yield return new WaitForSeconds(displayTime);
        messageText.gameObject.SetActive(false);
        currentCoroutine = null;
    }
}