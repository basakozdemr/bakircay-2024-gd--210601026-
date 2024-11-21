using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    private Camera cam;
    private Rigidbody rb;
    private bool isDragging = false;
    private Vector3 targetPosition;
    private float liftHeight = 2f; // Havaya kaldýrma yüksekliði
    private float smoothSpeed = 15f; // Lerp hýzýný kontrol eden deðer

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        // Orijinal pozisyonu al ve fiziði devre dýþý býrak
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        // Obje havaya kalkacaðý hedef pozisyonu belirle
        targetPosition = new Vector3(transform.position.x, liftHeight, transform.position.z);
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        // Fare pozisyonunu al ve hedef pozisyonu güncelle
        Vector3 mousePosition = GetMouseWorldPosition();
        targetPosition = new Vector3(mousePosition.x, liftHeight, mousePosition.z);
    }

    private void OnMouseUp()
    {
        // Objeyi býrakýldýðýnda yere dönmesi için fiziði etkinleþtir
        rb.useGravity = true;
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            // Obje, hedef pozisyona doðru yumuþak bir þekilde hareket ediyor
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Fare pozisyonunu dünya koordinatlarýna çevir
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.WorldToScreenPoint(transform.position).z;
        return cam.ScreenToWorldPoint(mousePoint);
    }


}
