using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("Boss");
            
            collision.gameObject.transform.position = new Vector2(0, 0);
        }
    }  
}
