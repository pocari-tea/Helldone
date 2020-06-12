using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RoomInfo : MonoBehaviour
{
    public bool is_cleared = false;
    public Vector2 gridPos;

    private void Start()
    {
        ParticleSystem[] ps = gameObject.transform.GetComponentsInChildren<ParticleSystem>(true);
        foreach (ParticleSystem _ in ps) {
            _.Play();
            if (!_.isPlaying)
            {
                Debug.Log(_.transform.parent.name);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!is_cleared && collision.CompareTag("Player"))
        {
            SpawnEnermy[] SE = gameObject.transform.GetComponentsInChildren<SpawnEnermy>();
            foreach(SpawnEnermy se in SE)
            {
                se.Summon();
            }
        }
    }

    public void Clear_Room()
    {
        is_cleared = true;

        ParticleSystem[] ps = gameObject.transform.GetComponentsInChildren<ParticleSystem>(true);
        foreach (ParticleSystem _ in ps)
        {
            _.Play(true);
        }
        Transform rc = gameObject.transform.Find("R collider");
        if(rc != null)
        {
            rc.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        Transform tc = gameObject.transform.Find("T Collider");
        if (tc != null)
        {
            tc.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        Transform bc = gameObject.transform.Find("B Collider");
        if (bc != null)
        {
            bc.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        Transform lc = gameObject.transform.Find("L Collider");
        if (lc != null)
        {
            lc.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
