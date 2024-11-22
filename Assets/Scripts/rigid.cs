using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigid : MonoBehaviour
{
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = true;
        }
    }

    void Update()
    {
        
    }
}
