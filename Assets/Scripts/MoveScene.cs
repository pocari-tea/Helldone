using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    string PathData = "Data";
    public void StartButtonClicked()
    {
        SceneManager.LoadScene("MapMaking");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void DeathQuit()
    {
        PlayerInfo status = new PlayerInfo();
        string jsonData = ObjectToJson(status);
        CreateJsonFile(string.Format("{0}/{1}", Application.dataPath, PathData), "Character_Status", jsonData);
        Application.Quit();
    }
    public void BackToMainScene()
    {
        SceneManager.LoadScene("MainMenu");
        PlayerInfo status = new PlayerInfo();
        string jsonData = ObjectToJson(status);
        CreateJsonFile(string.Format("{0}/{1}", Application.dataPath, PathData), "Character_Status", jsonData);
    }

    public void GoMainScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void CreateJsonFile(string createPath, string FileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, FileName),
            FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
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
