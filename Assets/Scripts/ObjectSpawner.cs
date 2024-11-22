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

    private void Start()
    {
        SpawnMeyveler();
    }

    void SpawnMeyveler()
    {
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

            Vector3 spawnPosition = new Vector3 (Random.Range(-6, 6), Random.Range(1,6), Random.Range(0, 12));

            Instantiate(meyvePrefab, spawnPosition, Quaternion.identity);

            meyveIndeksleri.RemoveAt(randomIndex);
        }
    }
}
