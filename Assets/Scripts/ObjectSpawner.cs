using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] meyvePrefabs; // Meyve prefablar�
    public int toplamMeyveSayisi = 20; // Toplam meyve say�s� (2x10 = 20)
    public float spawnHeight = 10f; // Meyvelerin spawn y�ksekli�i
    public float spawnAreaWidth = 10f; // Spawn alan�n�n geni�li�i
    public float spawnAreaHeight = 10f; // Spawn alan�n�n y�ksekli�i

    private Dictionary<GameObject, int> meyveIDMapping; // Her prefab i�in benzersiz ID e�lemesi

    private void Start()
    {
        InitializeMeyveIDMapping(); // Meyve t�rlerini ID'lerle ili�kilendir
        SpawnMeyveler();
    }

    void Update()
    {
        // E�er t�m objeler yok olduysa yeniden spawn et
        if (GameObject.FindGameObjectsWithTag("Draggable").Length == 0)
        {
            SpawnMeyveler();
        }
    }

    void InitializeMeyveIDMapping()
    {
        meyveIDMapping = new Dictionary<GameObject, int>();

        for (int i = 0; i < meyvePrefabs.Length; i++)
        {
            meyveIDMapping[meyvePrefabs[i]] = i + 1; // Her prefab i�in benzersiz bir ID
        }
    }

    void SpawnMeyveler()
    {
        // Prefab kontrol�
        if (meyvePrefabs == null || meyvePrefabs.Length == 0)
        {
            Debug.LogError("meyvePrefabs listesi bo�! L�tfen Inspector'da prefab'lar ekleyin.");
            return;
        }

        // Meyve t�rlerinin her birinden 2 adet olacak �ekilde spawn et
        List<int> meyveIndeksleri = new List<int>();

        for (int i = 0; i < meyvePrefabs.Length; i++)
        {
            meyveIndeksleri.Add(i);
            meyveIndeksleri.Add(i);
        }

        for (int i = 0; i < toplamMeyveSayisi; i++)
        {
            int randomIndex = Random.Range(0, meyveIndeksleri.Count);
            int meyveIndex = meyveIndeksleri[randomIndex];

            GameObject meyvePrefab = meyvePrefabs[meyveIndex];

            // Rastgele spawn pozisyonu
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2),
                spawnHeight,
                Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2)
            );

            GameObject spawnedObject = Instantiate(meyvePrefab, spawnPosition, Quaternion.identity);
            ConfigureRigidbody(spawnedObject);

            // Ayn� t�r i�in ayn� ID'yi atama
            AssignUniqueID(spawnedObject, meyveIDMapping[meyvePrefab]);

            meyveIndeksleri.RemoveAt(randomIndex);
        }
    }

    // Objelere benzersiz ID atama
    void AssignUniqueID(GameObject obj, int uniqueID)
    {
        obj.name = obj.name + "_" + uniqueID;
        ObjectID objectID = obj.AddComponent<ObjectID>();
        objectID.id = uniqueID;
    }

    // Rigidbody ekleme ve ayarlama
    void ConfigureRigidbody(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = obj.AddComponent<Rigidbody>();
        }

        rb.useGravity = true;
        rb.isKinematic = false;
    }
}

// Benzersiz ID tutmak i�in basit bir s�n�f
public class ObjectID : MonoBehaviour
{
    public int id;
}
