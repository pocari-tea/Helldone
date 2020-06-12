    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Int : MonoBehaviour
{
    private Rigidbody2D myRigidBody;

    [SerializeField] Transform champ;
    [SerializeField] float speed;

    [SerializeField] private Transform w_tr;

    private float mouse;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

        mouse = Input.mousePosition.x;
        float z = w_tr.localRotation.eulerAngles.z;

        Vector2 t_mouse_Pos = Input.mousePosition;
        Vector2 t_direction = new Vector2(t_mouse_Pos.x - champ.position.x,
                                          t_mouse_Pos.y - champ.position.y);

        champ.right = t_direction;


        if (mouse >= 400)
        {
            myRigidBody.GetComponent<Rigidbody2D>().velocity = myRigidBody.transform.right * speed;
        }
        else
        {
            myRigidBody.GetComponent<Rigidbody2D>().velocity = -myRigidBody.transform.right * speed;
        }
    }
}

