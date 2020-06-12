using System;
using System.IO;
using System.Text;
using UnityEngine;

public class Character_Job : MonoBehaviour
{
    public void SetWarrior()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        json.Job = "Warrior";
        playerInfo(json);
        string jsonData = ObjectToJson(json);
        CreateJsonFile(Application.dataPath + "/Data", "Character_Status", jsonData);
    }
    void SetArchor()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        json.Job = "Archor";
        playerInfo(json);
        string jsonData = ObjectToJson(json);
        CreateJsonFile(Application.dataPath + "/Data", "Character_Status", jsonData);
    }
    void SetMagician()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        json.Job = "Magician";
        playerInfo(json);
        string jsonData = ObjectToJson(json);
        CreateJsonFile(Application.dataPath + "/Data", "Character_Status", jsonData);
    }
    void SetLog()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        json.Job = "Log";
        playerInfo(json);
        string jsonData = ObjectToJson(json);
        CreateJsonFile(Application.dataPath + "/Data", "Character_Status", jsonData);
    }
    PlayerInfo playerInfo(PlayerInfo playerInfo)
    {

        if (playerInfo.Job == "Warrior")
        {
            playerInfo.Job = "Warrior";
            playerInfo.ATK = playerInfo.ATK + 3;
            playerInfo.HP = playerInfo.HP + 5;
        }
        else if (playerInfo.Job == "Archor")
        {
            playerInfo.Job = "Archor";
            playerInfo.ATK = playerInfo.ATK + 3;
            playerInfo.ATKSpeed = playerInfo.ATKSpeed * 1.3f;
        }
        else if (playerInfo.Job == "Magician")
        {
            playerInfo.Job = "Magician";
            playerInfo.ATK = playerInfo.ATK + 3;
            playerInfo.MP = playerInfo.MP + 5;
        }
        else if (playerInfo.Job == "Log")
        {
            playerInfo.Job = "Log";
            playerInfo.ATK = playerInfo.ATK + 3;
            playerInfo.MoveSpeed = playerInfo.MoveSpeed * 1.3f;
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