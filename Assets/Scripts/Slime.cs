using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public GameObject player;
    public float moving_distance = 5.0f;
    public float moving_term = 1.0f;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Invoke("Move", moving_term);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Move()
    {
        anim.SetTrigger("Move");
    }
}
