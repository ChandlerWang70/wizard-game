using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvItemPickup : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves the specific object you want
        if (collision.gameObject.CompareTag("Player"))
       {
            Destroy(gameObject);
            // Enable kinematic mode
            rb.isKinematic = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the collision involves the specific object you want
        if (collision.gameObject.CompareTag("Player"))
        {
            
        }
    }
    
}

