using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private static string previousScene;

    // Sahneyi de�i�tirmek i�in �a��r�lacak fonksiyon
    public void SwitchToScene(string sceneName)
    {
        // Ge�erli sahneyi sakla
        previousScene = SceneManager.GetActiveScene().name;
        // Yeni sahneye ge�
        SceneManager.LoadScene(sceneName);
    }

    // �nceki sahneye geri d�nmek i�in �a��r�lacak fonksiyon
    public void ReturnToPreviousScene()
    {
        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
    }
}
