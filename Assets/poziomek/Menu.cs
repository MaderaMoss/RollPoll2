using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [Header("Nazwy scen - ZMIEÑ JE NA SWOJE!")]
    public string sceneGame = "Game";           
    public string sceneSettings = "Settings";
    public string sceneCredits = "Credits";

    // ================== PRZYCISKI ==================

    public void NewGame()
    {
        SceneManager.LoadScene(sceneGame);
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene(sceneSettings);
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene(sceneCredits);
    }

    public void QuitGame()
    {
        Debug.Log("Wyjœcie z gry...");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // ================== POWRÓT DO MENU (dla Settings i Credits) ==================
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");  
    }
}