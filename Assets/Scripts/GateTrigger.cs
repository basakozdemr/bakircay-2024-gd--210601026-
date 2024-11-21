using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public Transform snapPosition; // Nesnenin taþýnacaðý hedef pozisyon
    private bool isSnapped = false; // Nesnenin sabitlenip sabitlenmediðini kontrol et

    private void OnTriggerEnter(Collider other)
    {
        // Sadece "Draggable" tag'ýna sahip nesneleri iþle
        if (other.CompareTag("Draggable"))
        {
            // Nesneyi sabitle
            other.transform.position = snapPosition.position;
            other.transform.rotation = snapPosition.rotation;

            // Fiziksel etkileri sýfýrla (Rigidbody varsa)
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true; // Fiziksel etkileþimleri geçici olarak devre dýþý býrak
            }

            isSnapped = true; // Nesne sabitlendi
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Nesne hala trigger alanýnda, pozisyonunu sürekli güncelle
        if (other.CompareTag("Draggable") && isSnapped)
        {
            other.transform.position = snapPosition.position;
            other.transform.rotation = snapPosition.rotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Nesne trigger alanýndan çýkarsa, sabitlemeyi kaldýr
        if (other.CompareTag("Draggable"))
        {
            isSnapped = false;

            // Nesneyi serbest býrak
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Fiziksel etkileþimleri tekrar aktif et
            }
        }
    }
}
