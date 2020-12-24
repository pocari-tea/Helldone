using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossController : MonoBehaviour
{
    public float hp;
    
    //발사될 위치
    [SerializeField] GameObject icePos;
    //방향
    [SerializeField] Transform Center;
    [SerializeField] private GameObject target;
    //총알
    [SerializeField] private GameObject ice;
    [SerializeField] private GameObject fire_pos;
    
    [SerializeField] private GameObject ClearPanel;

    private Animator anim;
    
    public static int patton;
    public static float patton_cool;
    private float time = 0.5f;
    
    public int nextAttack;
    private static readonly int Attack = Animator.StringToHash("attack");

    private void Start()
    {
        anim = GetComponent<Animator>();
        
        nextAttack = Random.Range(0, 3);
        NextPatton();
    }

    private void FixedUpdate()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
            ClearPanel.gameObject.SetActive(true);
        }
    }

    private void NextPatton()
    {
        if (nextAttack == 0)
        {
            StartCoroutine(Patton0 (10f));
        }
        if (nextAttack == 1)
        {
            StartCoroutine(Patton1 (10f));
        }
        if (nextAttack == 2)
        {
            StartCoroutine(Patton2 (5f));
        }
    }

    IEnumerator Patton0 (float cool)
    {
        while (cool > 0f)
        {
            if (patton_cool <= 0)
            {
                // 공격 모션
                anim.SetBool(Attack, true);
                // 마법진 생성
                icePos.SetActive(true);
                // Ice 생성
                GameObject Ice = Instantiate(ice);

                // Ice 생성 위치는 마법진
                Ice.transform.position = icePos.transform.position;
                var transformRotation = Center.rotation;
                transformRotation.z += Random.Range(-0.2f, 0.2f);
                // Ice의 방향을 Center의 방향으로
                Ice.transform.rotation = transformRotation;

                patton_cool = 0.5f;
            }
            
            cool -= Time.deltaTime;
            patton_cool -= Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        anim.SetBool(Attack, false);
        icePos.SetActive(false);
        nextAttack = Random.Range(0, 3);
        NextPatton();
    }
    
    IEnumerator Patton1 (float cool)
    {
        while (cool > 0f)
        {
            if (patton_cool <= 0)
            {
                // 공격 모션
                anim.SetBool(Attack, true);
                // Fire 마법진 생성
                GameObject Fire_pos = Instantiate(fire_pos);
                Fire_pos.transform.position = target.transform.position;
                // 2초 후에 fire 생성

                patton_cool = 3f;
            }
            
            cool -= Time.deltaTime;
            patton_cool -= Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }
        
        anim.SetBool(Attack, false);
        nextAttack = Random.Range(0, 3);
        NextPatton();
    }
    
    IEnumerator Patton2 (float cool)
    {
        while (cool > 0f)
        {
            if (patton_cool <= 0)
            {
                // 랜덤 위치 텔레포트
                gameObject.transform.position = new Vector2(Random.Range(-14f, 14f), Random.Range(-8.5f, 6f));
        
                patton_cool = 5f;
            }
            
            cool -= Time.deltaTime;
            patton_cool -= Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }
        
        nextAttack = Random.Range(0, 3);
        NextPatton();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Weapon_Effect"))
        {
            hp -= 10;
        }
    }
}
