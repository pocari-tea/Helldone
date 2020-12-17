using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{
    [SerializeField] private Image HP_bar;
    
    private void Start()
    {
        HP_bar = GameObject.Find("HPImageFull").GetComponent<Image>();
    }

    private void Update()
    {
        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        HP_bar.fillAmount = playerController.hp / 100f;
    }
}
