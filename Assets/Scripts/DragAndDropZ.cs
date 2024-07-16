using UnityEngine;

public class DragAndDropZ : MonoBehaviour
{
    private bool isDragging = false;
    private Rigidbody rb;
    private Vector3 offset;
    private Vector3 initialPosition; // Ba�lang�� pozisyonunu kaydetmek i�in
    private Collider collider; // Collider bile�eni i�in referans

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

        // Ba�lang�� pozisyonunu kaydet
        initialPosition = transform.position;

        // Collider bile�enini al
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        if (isDragging)
        {
            // Fare pozisyonunu d�nya koordinatlar�na �evir
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition) + offset;

            // Yeni pozisyonu sadece z ekseninde g�ncelle
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, worldPosition.z);

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

        // Collider'� tekrar etkinle�tir
        collider.enabled = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        // E�er s�r�kleniyorsa ve bir ba�ka birime temas ediyorsa
        if (isDragging)
        {
            // S�r�klemeyi durdur
            isDragging = false;

            // Rigidbody'nin h�z�n� s�f�rla
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Collider'� tekrar etkinle�tir
            collider.enabled = true;
        }
    }
}
