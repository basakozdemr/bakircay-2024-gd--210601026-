using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeyveSurukle : MonoBehaviour
{
    private bool isDragging = false;  // Meyve sürükleniyor mu?
    private Vector3 mouseOffset;  // Fare ile olan mesafe
    private Vector3 initialPosition;  // Baþlangýç pozisyonu
    private Rigidbody rb;  // Rigidbody bileþeni
    public float lerpSpeed = 10f;  // Lerp hýzý
    public float hoverHeight = 1f;  // Havaya kalkma yüksekliði
    public float returnSpeed = 2f;  // Geri inme hýzý
    private Vector3 targetPosition;
    private float originalYPosition;  // Baþlangýç Y pozisyonu

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        originalYPosition = transform.position.y;  // Baþlangýçta Y pozisyonunu kaydet
    }

    private void Update()
    {
        // Fare ile týklama iþlemi
        if (Input.GetMouseButtonDown(0))  // Týklama baþladýðýnda
        {
            if (IsMouseOverFruit())  // Meyve üzerine týklanýp týklanmadýðýný kontrol et
            {
                isDragging = true;
                mouseOffset = transform.position - GetMouseWorldPos();
            }
        }

        if (Input.GetMouseButtonUp(0))  // Týklama býrakýldýðýnda
        {
            if (isDragging)
            {
                isDragging = false;
            }
        }

        // Eðer sürükleniyorsa, nesneyi fare ile takip et
        if (isDragging)
        {
            targetPosition = GetMouseWorldPos() + mouseOffset;
            // Yumuþak hareket (lerp)
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);

            // Havaya kalkma (Y pozisyonunu yükselt)
            transform.position = new Vector3(transform.position.x, originalYPosition + hoverHeight, transform.position.z);
        }
        else
        {
            // Meyve býrakýldýðýnda, Y pozisyonunu sabit tutarak geri iner
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, initialPosition.y, Time.deltaTime * returnSpeed), transform.position.z);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        // Fare pozisyonunu dünya koordinatlarýna çevir
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;  // Kamera ile uyumlu hale getir
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private bool IsMouseOverFruit()
    {
        // Meyve üzerine týklanýp týklanmadýðýný kontrol et
        Collider collider = GetComponent<Collider>();
        return collider.bounds.Contains(GetMouseWorldPos());
    }
}
