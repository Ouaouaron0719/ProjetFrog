using UnityEngine;
using TMPro;
using System.Collections;

public class UIMessageManager : MonoBehaviour
{
    public static UIMessageManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private float displayTime = 2f;

    private Coroutine currentCoroutine;

    private void Awake()
    {
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

        currentCoroutine = StartCoroutine(HideAfterTime());
    }

    private IEnumerator HideAfterTime()
    {
        yield return new WaitForSeconds(displayTime);
        messageText.gameObject.SetActive(false);
        currentCoroutine = null;
    }
}