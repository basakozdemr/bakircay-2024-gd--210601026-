using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Camera cam;
    private Rigidbody rb;
    private Renderer objectRenderer;

    private bool isDragging = false;
    private bool isSnapping = false;

    private Vector3 targetPosition;
    private Vector3 snapTarget;
    private Vector3 offset;

    private float liftHeight = 5f;
    private float smoothSpeed = 15f;
    private float moveSpeed = 5f;

    private float minX = -5f;
    private float maxX = 5f;
    private float minZ = -10f;
    private float maxZ = 10f;
    private float minY = -2f;

    private static GameObject currentObjectInPlacementArea;

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        objectRenderer = GetComponent<Renderer>();

        if (rb != null)
        {
            rb.useGravity = true;
        }
    }

    private void OnMouseDown()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        targetPosition = new Vector3(transform.position.x, liftHeight, transform.position.z);
        isDragging = true;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = cam.WorldToScreenPoint(transform.position).z;
        offset = transform.position - cam.ScreenToWorldPoint(mousePosition);
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = cam.WorldToScreenPoint(transform.position).z;
        Vector3 worldPosition = cam.ScreenToWorldPoint(mousePosition) + offset;
        targetPosition = new Vector3(worldPosition.x, liftHeight, worldPosition.z);
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
        else if (isSnapping)
        {
            float newYPosition = Mathf.Lerp(transform.position.y, snapTarget.y + liftHeight, Time.deltaTime * moveSpeed);
            Vector3 snapPosition = new Vector3(snapTarget.x, newYPosition, snapTarget.z);
            transform.position = Vector3.Lerp(transform.position, snapPosition, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.position, snapPosition) < 0.7f)
            {
                isSnapping = false;
                CheckForMatchingObjects();
            }
        }

        if (IsOutOfBounds(transform.position))
        {
            RespawnToCenter();
        }
    }

    private bool IsOutOfBounds(Vector3 position)
    {
        return position.x < minX || position.x > maxX || position.z < minZ || position.z > maxZ || position.y < minY;
    }

    public void RespawnToCenter()
    {
        Debug.Log("Object out of bounds! Respawning...");
        transform.position = new Vector3(0, 1, 0);
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cylinder"))
        {
            if (currentObjectInPlacementArea != null && currentObjectInPlacementArea?.name != gameObject?.name)
            {
                transform.position = new Vector3(0, 1, 0);
                return;
            }

            snapTarget = other.transform.position;
            isSnapping = true;
            currentObjectInPlacementArea = gameObject;

            MatchManager matchManager = FindObjectOfType<MatchManager>();
            if (matchManager != null)
            {
                matchManager.SetObject(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cylinder"))
        {
            if (currentObjectInPlacementArea == gameObject)
            {
                currentObjectInPlacementArea = null;
                MatchManager matchManager = FindObjectOfType<MatchManager>();
                if (matchManager != null)
                {
                    matchManager.DeleteObject();
                }
            }

            isSnapping = false;
        }
    }

    private void CheckForMatchingObjects()
    {
        if (currentObjectInPlacementArea != null && currentObjectInPlacementArea != gameObject)
        {
            string currentObjectType = currentObjectInPlacementArea.name;
            string objectType = gameObject.name;

            if (currentObjectType == objectType)
            {
                Debug.Log("Matching objects placed! Increasing score...");
                GameManager.Instance.AddScore(10);
                currentObjectInPlacementArea = null;
            }
            else
            {
                transform.position = Vector3.zero;
            }
        }
    }
}
