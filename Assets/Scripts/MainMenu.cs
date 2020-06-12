using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartButtonClicked()
    {
        SceneManager.LoadScene("MapMaking");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
