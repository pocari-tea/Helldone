using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    public bool T, L, B, R = false; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.GetComponentInParent<RoomInfo>().is_cleared && collision.CompareTag("Player"))
        {
            GameObject.Find("PlayerManager").GetComponent<PlayerManager>().Room_Move(T, R, B, L);
        }

        new WaitForSeconds(0.1f);
    }
}
