using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    
    public GameObject objectPrefab; // Olu�turulacak nesnenin prefab�
    public int pairCount = 25; // �ift say�s�
    public Vector3 spawnArea = new Vector3(3, 0, 3); // Spawn alan� boyutlar�

    void Start()
    {
        for (int i = 1; i <= pairCount; i++) // Her �ift i�in d�ng�
        {
            for (int j = 0; j < 2; j++) // Her �iftin 2 adet nesnesi
            {
                // Rastgele bir konum belirle
                Vector3 randomPosition = new Vector3(
                    Random.Range(-spawnArea.x, spawnArea.x),
                    spawnArea.y,
                    Random.Range(-spawnArea.z, spawnArea.z)
                );

                // Nesne olu�tur
                GameObject newObject = Instantiate(objectPrefab, randomPosition, Quaternion.identity);

                // Nesneye ID atama
                newObject.GetComponent<Objects>().objectID = i;
            }
        }
    }
}
