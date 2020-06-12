using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnermy : MonoBehaviour
{
    public GameObject enermy;
    public GameObject particle;
    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = particle.GetComponent<ParticleSystem>();
    }

    public void Summon()
    {
        StartCoroutine("Create_Enermy");
    }

    private IEnumerator Create_Enermy()
    {
        Instantiate(particle);

        yield return new WaitForSeconds(ps.main.duration / 2);

        Instantiate(enermy);

        yield return null;
    }
}
