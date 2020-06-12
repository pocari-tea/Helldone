using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public PlayerManager pm;
    public Transform p_tr;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        p_tr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        camera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cpos = Vector3.zero;
        Vector2 map_size = pm.current_room.transform.Find("wall_background").GetComponent<SpriteRenderer>().size;
        if(Mathf.Abs(p_tr.position.x) % 100 + camera.orthographicSize <= map_size.x)
        {
            cpos.x = p_tr.position.x;
        }

        if (Mathf.Abs(p_tr.position.y) % 100 + camera.orthographicSize <= map_size.y)
        {
            cpos.y = p_tr.position.y;
        }
        cpos.z = -20;

        gameObject.transform.position = cpos;
    }
}
