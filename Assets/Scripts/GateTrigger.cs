using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public Transform snapPosition; // Nesnenin ta��naca�� hedef pozisyon
    private bool isSnapped = false; // Nesnenin sabitlenip sabitlenmedi�ini kontrol et

    private void OnTriggerEnter(Collider other)
    {
        // Sadece "Draggable" tag'�na sahip nesneleri i�le
        if (other.CompareTag("Draggable"))
        {
            // Nesneyi sabitle
            other.transform.position = snapPosition.position;
            other.transform.rotation = snapPosition.rotation;

            // Fiziksel etkileri s�f�rla (Rigidbody varsa)
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true; // Fiziksel etkile�imleri ge�ici olarak devre d��� b�rak
            }

            isSnapped = true; // Nesne sabitlendi
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Nesne hala trigger alan�nda, pozisyonunu s�rekli g�ncelle
        if (other.CompareTag("Draggable") && isSnapped)
        {
            other.transform.position = snapPosition.position;
            other.transform.rotation = snapPosition.rotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Nesne trigger alan�ndan ��karsa, sabitlemeyi kald�r
        if (other.CompareTag("Draggable"))
        {
            isSnapped = false;

            // Nesneyi serbest b�rak
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Fiziksel etkile�imleri tekrar aktif et
            }
        }
    }
}
