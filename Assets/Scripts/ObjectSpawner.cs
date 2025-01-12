using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] meyvePrefabs; // Meyve prefablarý
    public int toplamMeyveSayisi = 20; // Toplam meyve sayýsý (2x10 = 20)
    public float spawnHeight = 10f; // Meyvelerin spawn yüksekliði
    public float spawnAreaWidth = 10f; // Spawn alanýnýn geniþliði
    public float spawnAreaHeight = 10f; // Spawn alanýnýn yüksekliði

    private Dictionary<GameObject, int> meyveIDMapping; // Her prefab için benzersiz ID eþlemesi

    private void Start()
    {
        InitializeMeyveIDMapping(); // Meyve türlerini ID'lerle iliþkilendir
        SpawnMeyveler();
    }

    void Update()
    {
        // Eðer tüm objeler yok olduysa yeniden spawn et
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
            meyveIDMapping[meyvePrefabs[i]] = i + 1; // Her prefab için benzersiz bir ID
        }
    }

    void SpawnMeyveler()
    {
        // Prefab kontrolü
        if (meyvePrefabs == null || meyvePrefabs.Length == 0)
        {
            Debug.LogError("meyvePrefabs listesi boþ! Lütfen Inspector'da prefab'lar ekleyin.");
            return;
        }

        // Meyve türlerinin her birinden 2 adet olacak þekilde spawn et
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

            // Ayný tür için ayný ID'yi atama
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

// Benzersiz ID tutmak için basit bir sýnýf
public class ObjectID : MonoBehaviour
{
    public int id;
}
