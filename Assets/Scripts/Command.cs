using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Command : MonoBehaviour
{
    [SerializeField] InputField textInput;

    public void TextButtonClicked()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        playerInfo(json, textInput.text);
        string jsonData = ObjectToJson(json);
        CreateJsonFile(Application.dataPath + "/Data", "Character_Status", jsonData);
    }

    PlayerInfo playerInfo(PlayerInfo playerInfo, string text)
    {

        if (text == "MaxHP")
        {
            playerInfo.HP = 10000;
        }
        else if (text == "MaxATK")
        {
            playerInfo.ATK = 10000;
        }
        else if (text == "MaxMP")
        {
            playerInfo.MP = 10000;
        }
        else if (text == "MaxDEF")
        {
            playerInfo.DEF = 10000;
        }
        return playerInfo;
    }

    void CreateJsonFile(string createPath, string FileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, FileName),
            FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
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

    string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }
}
