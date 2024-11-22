using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Camera cam;
    private Rigidbody rb;
    private bool isDragging = false;
    private Vector3 targetPosition;
    private float liftHeight = 5f; 
    private float smoothSpeed = 15f;

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        rb.useGravity = false; 
        rb.velocity = Vector3.zero; 
        rb.isKinematic = true; 

        targetPosition = new Vector3(transform.position.x, liftHeight, transform.position.z);
        isDragging = true; 
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        targetPosition = new Vector3(mousePosition.x, liftHeight, mousePosition.z);
    }

    private void OnMouseUp()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.WorldToScreenPoint(transform.position).z;
        return cam.ScreenToWorldPoint(mousePoint);
    }


}
