using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    private Camera cam;
    private Rigidbody rb;
    private bool isDragging = false;
    private Vector3 targetPosition;
    private float liftHeight = 2f; // Havaya kald�rma y�ksekli�i
    private float smoothSpeed = 15f; // Lerp h�z�n� kontrol eden de�er

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        // Orijinal pozisyonu al ve fizi�i devre d��� b�rak
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        // Obje havaya kalkaca�� hedef pozisyonu belirle
        targetPosition = new Vector3(transform.position.x, liftHeight, transform.position.z);
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        // Fare pozisyonunu al ve hedef pozisyonu g�ncelle
        Vector3 mousePosition = GetMouseWorldPosition();
        targetPosition = new Vector3(mousePosition.x, liftHeight, mousePosition.z);
    }

    private void OnMouseUp()
    {
        // Objeyi b�rak�ld���nda yere d�nmesi i�in fizi�i etkinle�tir
        rb.useGravity = true;
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            // Obje, hedef pozisyona do�ru yumu�ak bir �ekilde hareket ediyor
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Fare pozisyonunu d�nya koordinatlar�na �evir
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.WorldToScreenPoint(transform.position).z;
        return cam.ScreenToWorldPoint(mousePoint);
    }


}
