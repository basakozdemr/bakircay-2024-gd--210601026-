using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderController : MonoBehaviour
{
    private GameObject currentFruit; // Þu anda silindirin üzerindeki meyve

    public bool IsOccupied()
    {
        return currentFruit != null; // Silindir dolu mu kontrol et
    }

    private void OnTriggerEnter(Collider other)
    {
        // Eðer silindire bir meyve girerse ve boþsa
        if (currentFruit == null && other.CompareTag("Fruit"))
        {
            currentFruit = other.gameObject; // Meyveyi kaydet
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Eðer mevcut meyve silindiri terk ederse
        if (other.gameObject == currentFruit)
        {
            currentFruit = null; // Meyveyi sýfýrla
        }
    }
}
