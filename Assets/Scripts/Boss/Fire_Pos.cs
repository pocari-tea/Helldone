using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Pos : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    private void FixedUpdate()
    {
        Destroy(gameObject, 2f);
    }

    private void Start()
    {
        Invoke("Fire", 2);
    }

    private void Fire()
    {
        GameObject Fire = Instantiate(fire);
        //플레이어 위치에 불꽃 생성
        Fire.transform.position = gameObject.transform.position;
    }
}
