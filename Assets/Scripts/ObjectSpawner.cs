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

        // Her meyve t�r�nden 2 adet olu�tur
        for (int i = 0; i < meyvePrefabs.Length; i++)
        {
            meyveIndeksleri.Add(i);
            meyveIndeksleri.Add(i); // Her meyveden 2 tane olacak �ekilde ekle
        }

        // Toplamda 20 meyve spawn etmek i�in d�ng�
        for (int i = 0; i < toplamMeyveSayisi; i++)
        {
            // Rastgele bir meyve t�r� se�
            int randomIndex = Random.Range(0, meyveIndeksleri.Count);
            int meyveIndex = meyveIndeksleri[randomIndex];

            // Se�ilen meyve t�r�nden bir prefab se�
            GameObject meyvePrefab = meyvePrefabs[meyveIndex];

            // Rastgele bir spawn pozisyonu belirle
            Vector3 spawnPosition = new Vector3 (Random.Range(-3, 3), 6, Random.Range(-3, 3));

            // Meyve prefab'�n� spawn et
            Instantiate(meyvePrefab, spawnPosition, Quaternion.identity);

            // Se�ilen meyve t�r�n� listeden ��kar
            meyveIndeksleri.RemoveAt(randomIndex);
        }
    }
}
