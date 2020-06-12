using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SortOfInheritance
{
    public string gigantism; // 거인증
    public string blind; // 색맹
    public string predisposition; // 소인증
    public string nyctalopia; // 야맹증
    public string schizophrenia; // 정신분열증
    public SortOfInheritance()
    {
        gigantism = "Gigantism";
        blind = "Blind";
        predisposition = "Predisposition";
        nyctalopia = "Nyctalopia";
        schizophrenia = "Schizophrenia";
    }
}

public class Inheritance : MonoBehaviour
{
    [SerializeField] Image Nyctalopia;
    [SerializeField] Image Blind;
    [SerializeField] GameObject Player;
    
    string PathData = "Data";
    private void Start()
    {
        string jsonFileName = "Data/SortOfInheritance.json";
        string strFile = string.Format("{0}/{1}", Application.dataPath, jsonFileName);
        if (File.Exists(strFile))
        {}
        else
        {
            SortOfInheritance status = new SortOfInheritance();
            string jsonData = ObjectToJson(status);
            CreateJsonFile(string.Format("{0}/{1}", Application.dataPath, PathData), "SortOfInheritance", jsonData);
        }
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

    private void Awake()
    {
        if(UseJsonFile().Heredity == "Blind")
        {
            Blind.gameObject.SetActive(true);
        }
        else if (UseJsonFile().Heredity == "Nyctalopia")
        {
            Nyctalopia.gameObject.SetActive(true);
        }
    }

    PlayerInfo UseJsonFile()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        return json;
    }
}
