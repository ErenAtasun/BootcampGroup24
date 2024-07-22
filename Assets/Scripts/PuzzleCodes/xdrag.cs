using UnityEngine;

public class xdrag : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody rb;
    private bool isColliding = false;
    private Vector3 lastValidPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Rigidbody'nin fizik motoru taraf�ndan kontrol edilmemesi i�in isKinematic'i true yap
        }
    }

    void Update()
    {
        // Sol t�klamay� kontrol et
        if (Input.GetMouseButtonDown(0))
        {
            // Ekrandaki fare pozisyonunu belirler
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Birim ile �arp��may� kontrol et
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                isDragging = true;
                // Birim ile farenin aras�ndaki mesafeyi belirler
                offset = transform.position - hit.point;
                lastValidPosition = transform.position; // Ba�lang�� konumunu kaydet
            }
        }

        // Sol t�klamay� b�rakmay� kontrol et
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            if (isColliding)
            {
                transform.position = lastValidPosition; // �arp��ma varsa son ge�erli pozisyona d�n
                isColliding = false; // �arp��ma durumunu s�f�rla
            }
        }

        // Birimi s�r�kle
        if (isDragging && !isColliding)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Ekrandaki fare pozisyonunu belirler
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 newPosition = new Vector3(hit.point.x + offset.x, transform.position.y, transform.position.z); // Y pozisyonunu sabit tut, sadece X pozisyonunu de�i�tir
                if (rb != null)
                {
                    rb.MovePosition(newPosition); // Rigidbody'yi yeni pozisyona hareket ettir
                }
                else
                {
                    transform.position = newPosition; // Rigidbody yoksa direkt pozisyonu de�i�tir
                }
                lastValidPosition = transform.position; // Ge�erli pozisyonu kaydet
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDragging)
        {
            isColliding = true; // �arp��ma ba�lad���nda �arp��ma durumunu ayarla
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isDragging)
        {
            isColliding = false; // �arp��ma sona erdi�inde �arp��ma durumunu s�f�rla
        }
    }
}
