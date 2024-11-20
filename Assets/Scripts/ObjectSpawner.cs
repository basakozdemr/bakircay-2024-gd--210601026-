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

    private void Start()
    {
        SpawnMeyveler();
    }

    void SpawnMeyveler()
    {
        // Meyve türlerinin her birinden 2 adet olacak þekilde spawn et
        List<int> meyveIndeksleri = new List<int>();

        // Her meyve türünden 2 adet oluþtur
        for (int i = 0; i < meyvePrefabs.Length; i++)
        {
            meyveIndeksleri.Add(i);
            meyveIndeksleri.Add(i); // Her meyveden 2 tane olacak þekilde ekle
        }

        // Toplamda 20 meyve spawn etmek için döngü
        for (int i = 0; i < toplamMeyveSayisi; i++)
        {
            // Rastgele bir meyve türü seç
            int randomIndex = Random.Range(0, meyveIndeksleri.Count);
            int meyveIndex = meyveIndeksleri[randomIndex];

            // Seçilen meyve türünden bir prefab seç
            GameObject meyvePrefab = meyvePrefabs[meyveIndex];

            // Rastgele bir spawn pozisyonu belirle
            Vector3 spawnPosition = new Vector3 (Random.Range(-3, 3), 6, Random.Range(-3, 3));

            // Meyve prefab'ýný spawn et
            Instantiate(meyvePrefab, spawnPosition, Quaternion.identity);

            // Seçilen meyve türünü listeden çýkar
            meyveIndeksleri.RemoveAt(randomIndex);
        }
    }
}
