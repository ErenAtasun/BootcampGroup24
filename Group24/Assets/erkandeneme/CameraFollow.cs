using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Oyuncu karakterinin transform bile�eni
    public Vector3 offset; // Kameran�n oyuncuya g�re olan mesafesi

    // LateUpdate, Update'den sonra �a�r�l�r ve kamera hareketleri i�in daha uygundur
    void LateUpdate()
    {
        // Kameray� oyuncunun konumuna offset ekleyerek ayarla
        transform.position = player.position + offset;
    }
}
