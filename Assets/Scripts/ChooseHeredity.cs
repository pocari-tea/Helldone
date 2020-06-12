using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ChooseHeredity : MonoBehaviour
{
    private Transform tr;

    static public Button GigantismButton;
    static public Button BlindButton;
    static public Button PredispositionButton;
    static public Button NyctalopiaButton;
    static public Button SchizophreniaButton;

    public List<Button> buttons = new List<Button> { GigantismButton, BlindButton, PredispositionButton, NyctalopiaButton, SchizophreniaButton };
    private void Awake()
    {
        Vector3 pos = new Vector3(-250, 0, 0);
        for(int i = 0; i <3 ;i++)
        {
            int rand = Random.Range(0, buttons.Count);
            buttons[rand].transform.localPosition = pos;
            buttons[rand].gameObject.SetActive(true);
            pos.x += 250;
            buttons.RemoveAt(rand);
        }
    }

    public void setGigantism()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        json.Heredity = "Gigantism";
        string jsonData = ObjectToJson(json);
        CreateJsonFile(Application.dataPath + "/Data", "Character_Status", jsonData);
    }

    public void setBlind()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        json.Heredity = "Blind";
        string jsonData = ObjectToJson(json);
        CreateJsonFile(Application.dataPath + "/Data", "Character_Status", jsonData);
    }

    public void setPredisposition()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        json.Heredity = "Predisposition";
        string jsonData = ObjectToJson(json);
        CreateJsonFile(Application.dataPath + "/Data", "Character_Status", jsonData);
    }

    public void setNyctalopia()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        json.Heredity = "Nyctalopia";
        string jsonData = ObjectToJson(json);
        CreateJsonFile(Application.dataPath + "/Data", "Character_Status", jsonData);
    }

    public void setSchizophrenia()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        json.Heredity = "Schizophrenia";
        string jsonData = ObjectToJson(json);
        CreateJsonFile(Application.dataPath + "/Data", "Character_Status", jsonData);
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
}
