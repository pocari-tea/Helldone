using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Death : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
