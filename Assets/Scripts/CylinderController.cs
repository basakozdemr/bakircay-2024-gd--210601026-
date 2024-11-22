using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderController : MonoBehaviour
{
    private GameObject currentFruit; // �u anda silindirin �zerindeki meyve

    public bool IsOccupied()
    {
        return currentFruit != null; // Silindir dolu mu kontrol et
    }

    private void OnTriggerEnter(Collider other)
    {
        // E�er silindire bir meyve girerse ve bo�sa
        if (currentFruit == null && other.CompareTag("Fruit"))
        {
            currentFruit = other.gameObject; // Meyveyi kaydet
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // E�er mevcut meyve silindiri terk ederse
        if (other.gameObject == currentFruit)
        {
            currentFruit = null; // Meyveyi s�f�rla
        }
    }
}
