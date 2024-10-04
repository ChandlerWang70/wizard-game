using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellv1 : MonoBehaviour
{

    public float life = 3;

    void Awake() {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.CompareTag("Destroyable"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
