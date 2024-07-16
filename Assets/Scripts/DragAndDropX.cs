using UnityEngine;

public class DragAndDropX : MonoBehaviour
{
    private bool isDragging = false;
    private Rigidbody rb;
    private Vector3 offset;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        if (isDragging)
        {
            // Fare pozisyonunu d�nya koordinatlar�na �evir
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition) + offset;

            // Yeni pozisyonu sadece x ekseninde g�ncelle
            Vector3 newPosition = new Vector3(worldPosition.x, transform.position.y, transform.position.z);

            // Rigidbody ile pozisyonu g�ncelle
            rb.MovePosition(newPosition);
        }
    }

    void OnMouseDown()
    {
        // Fare ile t�klan�nca s�r�kleme ba�lat
        isDragging = true;

        // T�klama an�ndaki fare pozisyonu ile nesne aras�ndaki fark� hesapla
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
    }

    void OnMouseUp()
    {
        // Fare t�klamas� b�rak�l�nca s�r�kleme durdur
        isDragging = false;

        // S�r�klemeyi durdurdu�umuzda Rigidbody'nin h�z�n� s�f�rla
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision collision)
    {
        // �arp��ma durumunda s�r�klemeyi durdur ve son konumda kal
        if (isDragging)
        {
            isDragging = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
