using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    
    public GameObject objectPrefab; // Oluþturulacak nesnenin prefabý
    public int pairCount = 25; // Çift sayýsý
    public Vector3 spawnArea = new Vector3(3, 0, 3); // Spawn alaný boyutlarý

    void Start()
    {
        for (int i = 1; i <= pairCount; i++) // Her çift için döngü
        {
            for (int j = 0; j < 2; j++) // Her çiftin 2 adet nesnesi
            {
                // Rastgele bir konum belirle
                Vector3 randomPosition = new Vector3(
                    Random.Range(-spawnArea.x, spawnArea.x),
                    spawnArea.y,
                    Random.Range(-spawnArea.z, spawnArea.z)
                );

                // Nesne oluþtur
                GameObject newObject = Instantiate(objectPrefab, randomPosition, Quaternion.identity);

                // Nesneye ID atama
                newObject.GetComponent<Objects>().objectID = i;
            }
        }
    }
}
