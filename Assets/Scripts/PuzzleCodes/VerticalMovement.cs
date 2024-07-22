using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true; // Kinematic yaparak fiziksel hesaplamalar� devre d��� b�rak�yoruz
        }
    }

    private void OnMouseDown()
    {
        // Fare t�klama ba�lad���nda birimi se�mek i�in
        isDragging = true;
        // Se�ili objenin pozisyonuna offset hesaplamak i�in
        offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseUp()
    {
        // Fare t�klama b�rakt���nda birimi b�rakmak i�in
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 newPos = GetMouseWorldPos() + offset;
            newPos.x = transform.position.x; // X d�zleminde sabit tut
            transform.position = new Vector3(transform.position.x, transform.position.y, newPos.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Di�er birimlere �arpt���nda hareket etmeyi durdur
        isDragging = false;
        rb.velocity = Vector3.zero; // Hareket etmeyi tamamen durdur
        rb.angularVelocity = Vector3.zero; // D�nmeyi tamamen durdur
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.y; // Kamera y�ksekli�i ile mesafeyi belirleyin
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
