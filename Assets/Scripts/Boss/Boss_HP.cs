using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_HP : MonoBehaviour
{
    [SerializeField] private Image HP_bar;
    
    private void Start()
    {
        HP_bar = gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        BossController bossController = GameObject.Find("Boss").GetComponent<BossController>();

        HP_bar.fillAmount = bossController.hp / 200f;
        
        Debug.Log(bossController.hp);
    }
}
