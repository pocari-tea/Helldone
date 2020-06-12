using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    private PlayerPad controls;

    private Rigidbody2D rigid;
    private Transform tr;
    private Vector2 move;

    public float movePower = 10;
    public float jumpPower = 15;

    private int JumpCount;

    public Transform feetPos;
    public LayerMask whatIsGround;
    public float circleRadius;

    public Camera main_camera;
    private Vector2 mouse_cursor;


    private bool isJumping;
    private bool isGrounded;
    private bool isFalled;

    private Animator animator;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Jump();

        mouse_cursor = main_camera.ScreenToWorldPoint(Input.mousePosition);


        if(mouse_cursor.x > gameObject.transform.position.x)
        {
            tr.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            tr.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        if (isGrounded == true && gameObject.layer != 8)
        {
            gameObject.layer = 8;
        }

        if (Input.GetKey(KeyCode.S) && isGrounded == true)
        {
            gameObject.layer = 9;

            isFalled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFalled = false;

        if (Physics2D.Raycast(tr.position, Vector2.down, 2f) && collision.gameObject.tag == "Ground" && isFalled == false)
        {
            isGrounded = true;

            JumpCount = 2;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 && Input.GetKey(KeyCode.S))
        {
            JumpCount = 0;
        }
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            moveVelocity = Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveVelocity = Vector3.right;
        }
        transform.position += moveVelocity * movePower * Time.deltaTime;

        animator.SetBool("Move", Convert.ToBoolean(moveVelocity.x));
    }

    void Jump()
    {
        if (!isJumping)
        {
            return;
        }

        if (JumpCount > 0)
        {
            rigid.velocity = Vector2.zero;

            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

            isJumping = false;
            JumpCount--;

            animator.SetTrigger("Jump");
        }
    }
}
