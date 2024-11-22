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

            Vector3 spawnPosition = new Vector3 (Random.Range(-6, 6), Random.Range(1,6), Random.Range(0, 12));

            Instantiate(meyvePrefab, spawnPosition, Quaternion.identity);

            meyveIndeksleri.RemoveAt(randomIndex);
        }
    }
}
