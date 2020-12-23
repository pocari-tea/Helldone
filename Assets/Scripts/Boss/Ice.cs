using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    private float speed = 10f;
    private float damage = 20;
    
    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
