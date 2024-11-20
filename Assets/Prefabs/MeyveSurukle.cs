using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeyveSurukle : MonoBehaviour
{
    private bool isDragging = false;  // Meyve s�r�kleniyor mu?
    private Vector3 mouseOffset;  // Fare ile olan mesafe
    private Vector3 initialPosition;  // Ba�lang�� pozisyonu
    private Rigidbody rb;  // Rigidbody bile�eni
    public float lerpSpeed = 10f;  // Lerp h�z�
    public float hoverHeight = 1f;  // Havaya kalkma y�ksekli�i
    public float returnSpeed = 2f;  // Geri inme h�z�
    private Vector3 targetPosition;
    private float originalYPosition;  // Ba�lang�� Y pozisyonu

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        originalYPosition = transform.position.y;  // Ba�lang��ta Y pozisyonunu kaydet
    }

    private void Update()
    {
        // Fare ile t�klama i�lemi
        if (Input.GetMouseButtonDown(0))  // T�klama ba�lad���nda
        {
            if (IsMouseOverFruit())  // Meyve �zerine t�klan�p t�klanmad���n� kontrol et
            {
                isDragging = true;
                mouseOffset = transform.position - GetMouseWorldPos();
            }
        }

        if (Input.GetMouseButtonUp(0))  // T�klama b�rak�ld���nda
        {
            if (isDragging)
            {
                isDragging = false;
            }
        }

        // E�er s�r�kleniyorsa, nesneyi fare ile takip et
        if (isDragging)
        {
            targetPosition = GetMouseWorldPos() + mouseOffset;
            // Yumu�ak hareket (lerp)
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);

            // Havaya kalkma (Y pozisyonunu y�kselt)
            transform.position = new Vector3(transform.position.x, originalYPosition + hoverHeight, transform.position.z);
        }
        else
        {
            // Meyve b�rak�ld���nda, Y pozisyonunu sabit tutarak geri iner
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, initialPosition.y, Time.deltaTime * returnSpeed), transform.position.z);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        // Fare pozisyonunu d�nya koordinatlar�na �evir
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;  // Kamera ile uyumlu hale getir
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private bool IsMouseOverFruit()
    {
        // Meyve �zerine t�klan�p t�klanmad���n� kontrol et
        Collider collider = GetComponent<Collider>();
        return collider.bounds.Contains(GetMouseWorldPos());
    }
}
