using UnityEngine;
using UnityEngine.SceneManagement;

public class guardManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Paneli buraya ataca��z
    public string gameOverMessage = "Yakaland�n"; // Ekrandaki mesaj

    void Start()
    {
        // Oyunun ba��nda paneli gizle
        gameOverPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("beeGuard"))
        {
            Debug.Log("Beeguard ile temas edildi!");
            SceneManagment.instance.ReloadScene();
            //Cursor.visible = true; // Fare imlecini g�r�n�r yap
            //Cursor.lockState = CursorLockMode.None; // Fare imlecinin kilidini a�
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Zaman� eski haline getir
        Cursor.visible = false; // Fare imlecini gizle (iste�e ba�l�)
        Cursor.lockState = CursorLockMode.Locked; // Fare imlecini kilitle (iste�e ba�l�)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}