using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] Image fullHpIamge;
    [SerializeField] Image fullMpImage;
    [SerializeField] Image STRSkill;
    [SerializeField] Image DEXSkill;
    [SerializeField] Image INTSkill;
    [SerializeField] GameObject InputPanel;

    public GameObject BackPanel;
    bool BackPanelOn = false;


    string recentMostHighStatus;
    string MostHighStatus;
    bool IsInputFiled = false;

    private string checkMostHighStatus()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        if (json.STR > json.DEX)
        {
            if (json.STR > json.INT)
                MostHighStatus = "STR";
            else if (json.STR < json.INT)
                MostHighStatus = "INT";
            else
                MostHighStatus = null;
        }
        else if (json.STR < json.DEX)
        {
            if (json.DEX > json.INT)
                MostHighStatus = "DEX";
            else if (json.DEX < json.INT)
                MostHighStatus = "INT";
            else
                MostHighStatus = null;
        }
        else MostHighStatus = null;

        return MostHighStatus;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Semicolon))
        {
            SceneManager.LoadScene("MapMaking");
        }


        fullHpIamge.fillAmount = UseJsonFile().recentHP * 0.01f;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!IsInputFiled)
            {
                InputPanel.gameObject.SetActive(true);
                IsInputFiled = true;
            }
            else
            {
                InputPanel.gameObject.SetActive(false);
                IsInputFiled = false;
            }
                
        }
    }

    public void TurnOffGame()
    {
        if(BackPanelOn)
        {
            BackPanel.gameObject.SetActive(true);
            BackPanelOn = true; 
        }
        else
        {
            BackPanel.gameObject.SetActive(false);
            BackPanelOn = false;
        }
            
    }

    T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(jsonData);
    }
    PlayerInfo UseJsonFile()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        return json;
    }
}
