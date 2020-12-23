using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Aim : MonoBehaviour
{
    public Transform center;
    public Transform target;

    void FixedUpdate () {
        var rot = target.position - center.position;
        
        var angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
        
        center.rotation = Quaternion.Euler(0, 0, angle);
    }
}
