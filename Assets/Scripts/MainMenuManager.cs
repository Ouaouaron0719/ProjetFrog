using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string firstLevelSceneName = "SampleScene";
    private void Start()
    {
        Time.timeScale = 1f;

        if (DatabaseManager.Instance != null)
        {
            DatabaseManager.Instance.ResetAllAbilities();
            Debug.Log("Abilities reset from Main Menu");
        }
        else
        {
            Debug.LogWarning("DatabaseManager instance not found in Main Menu scene.");
        }
    }
    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(firstLevelSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}