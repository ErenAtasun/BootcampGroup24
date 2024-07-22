using UnityEngine;
using UnityEngine.UI; // UI bile�enleri i�in gerekli
using TMPro; // E�er TextMeshPro kullan�yorsan�z bu namespace gerekli

public class CongratulationsDisplay : MonoBehaviour
{
    public GameObject targetObject; // Temas edilecek GameObject
    public GameObject playerObject; // Temas eden GameObject
    public TextMeshProUGUI congratulationsText; // TextMeshPro i�in
    // public Text congratulationsText; // Text bile�eni i�in
    // public GameObject congratulationsTextObject; // E�er Text GameObject'ini referans olarak kullan�yorsan�z

    void Start()
    {
        // Ba�lang��ta yaz�y� gizle
        if (congratulationsText != null)
        {
            congratulationsText.enabled = false;
        }
        // E�er Text GameObject'ini referans olarak kullan�yorsan�z
        // if (congratulationsTextObject != null)
        // {
        //     congratulationsTextObject.SetActive(false);
        // }
    }

    void OnTriggerEnter(Collider other)
    {
        // E�er �arp��an nesne hedef nesne ise ve hedef nesne Player nesnesiyle temas ediyorsa
        if (other.gameObject == targetObject)
        {
            if (playerObject.GetComponent<Collider>().bounds.Intersects(targetObject.GetComponent<Collider>().bounds))
            {
                // Temas edildi�inde yaz�y� g�ster
                if (congratulationsText != null)
                {
                    congratulationsText.enabled = true;
                }
                // E�er Text GameObject'ini referans olarak kullan�yorsan�z
                // if (congratulationsTextObject != null)
                // {
                //     congratulationsTextObject.SetActive(true);
                // }
            }
        }
    }
}
