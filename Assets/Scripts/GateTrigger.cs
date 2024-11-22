using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public Transform centerPoint; 
    private GameObject currentObject; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Draggable"))
        {
            if (currentObject == null)
            {
                currentObject = other.gameObject;
                currentObject.transform.position = Vector3.Lerp(transform.position, centerPoint.position, Time.deltaTime * 1);
                currentObject.GetComponent<Rigidbody>().velocity = Vector3.zero; 
            }
            else
            {
                Rigidbody rb = other.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 forceDirection = (other.transform.position - transform.position).normalized;
                    rb.AddForce(forceDirection * 1000f);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentObject)
        {
            currentObject = null;
        }
    }
}
