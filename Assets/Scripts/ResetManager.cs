using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahne y�netimi i�in gerekli

public class ResetManager : MonoBehaviour
{
    // Reset butonuna ba�lanacak fonksiyon
    public void ResetGame()
    {
        // Skoru s�f�rla
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(-GameManager.Instance.GetScore()); // Skoru s�f�rla
        }

        // T�m objeleri temizle
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Draggable");
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }

        // Aktif sahneyi yeniden y�kle (oyunu ba�tan ba�lat)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        UnityEngine.Debug.Log("Oyun s�f�rland� ve yeniden ba�lat�ld�!");
    }
}
