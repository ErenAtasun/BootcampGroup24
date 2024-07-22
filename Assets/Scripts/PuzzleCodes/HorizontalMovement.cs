using UnityEngine;

public class HorizontalMovement : MonoBehaviour
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
            newPos.y = transform.position.y; // Y d�zleminde sabit tut
            transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Di�er birimlere �arpt���nda hareket etmeyi durdur
        isDragging = false;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.y; // Kamera y�ksekli�i ile mesafeyi belirleyin
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
