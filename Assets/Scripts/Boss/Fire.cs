using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private float damage = 30;

    private void FixedUpdate()
    {
        Destroy(gameObject, 2f);
    }
}
