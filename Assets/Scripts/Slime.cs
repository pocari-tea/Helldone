using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Slime : MonoBehaviour
{
    private float hp = 60;
    private Rigidbody2D rigid;
    public int nextMove;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        Invoke("Think", 5);
    }

    private void FixedUpdate()
    {    
        //Move
        rigid.velocity = new Vector2(nextMove * 2f, rigid.velocity.y );

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null)
            Turn();

        if (hp <= 0)
        {
            Destroy(this);
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
        //Fiip Sprite
        if (nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 0;
        }
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 0;
        
        CancelInvoke();
        Invoke("Think", 5);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Weapon_Effect"))
        {
            hp -= 30;
        }
    }
}
