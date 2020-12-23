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
    private Animator anim;
    private static readonly int Die = Animator.StringToHash("die");
    private static readonly int Move = Animator.StringToHash("Move");

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
        Invoke("Think", 5);
    }

    private void FixedUpdate()
    {    
        //Move
        rigid.velocity = new Vector2(nextMove * 2f, rigid.velocity.y );

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 3, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null)
            Turn();

        if (hp <= 0)
        {
            Destroy(gameObject);
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
            anim.SetBool(Move, true);
            spriteRenderer.flipX = nextMove == -1;
        }
        else
        {
            anim.SetBool(Move, false);
        }
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == -1;

        CancelInvoke();
        Invoke("Think", 2);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Weapon_Effect"))
        {
            hp -= 30;
        }
    }
}
