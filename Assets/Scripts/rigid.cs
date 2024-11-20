using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Meyve prefabýnýn Rigidbody bileþenini al
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Yerçekimini aktif et
            rb.useGravity = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
