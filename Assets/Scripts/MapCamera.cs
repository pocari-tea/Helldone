using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    [SerializeField] Camera MapCam;
    bool IsMapCam = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(IsMapCam)
            {
                MapCam.gameObject.SetActive(false);
                IsMapCam = false;
            }
            else
            {
                MapCam.gameObject.SetActive(true);
                IsMapCam = true;
            }
        }
    }
}
