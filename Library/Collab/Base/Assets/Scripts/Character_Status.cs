using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class PlayerInfo
{
    public int LV; // 레벨
    public int EXP; // 경험치
    public int HP; // 체력
    public int ATK; // 공격력
    public float ATKSpeed; // 공격속도
    public int MP; // 기력
    public int DEF; // 방어력
    public int Dodge; // 회피
    public int Critical; // 치명타
    public float MoveSpeed; // 이동속도
    public float CoolTime; // 재사용 대기 시간
    public string Job; // 직업
    public string Heredity; // 유전 능력치
    public PlayerInfo()
    {
        LV = 1; // 레벨
        EXP = 0; // 경험치
        HP = 10; // 체력
        ATK = 10; // 공격력
        ATKSpeed = 1.0f; // 공격속도
        MP = 10; // 기력
        DEF = 1; // 방어력
        Dodge = 5; // 회피
        Critical = 10; // 치명타
        MoveSpeed = 1.0f; // 이동속도
        CoolTime = 1.0f; // 재사용 대기 시간
        Job = "None"; // 직업
        Heredity = "None"; // 유전 능력치
    }

    public PlayerInfo(string job)
    {
        if(job == "Warrior")
        {
            Job = "Warrior";
            ATK = ATK + 3;
            HP = HP + 5;
        }
        else if(job == "Archor")
        {
            Job = "Archor";
            ATK = ATK + 3;
            ATKSpeed = ATKSpeed * 1.3f;
        }
        else if(job == "Magician")
        {
            Job = "Magician";
            ATK = ATK + 3;
            MP = MP + 5;
        }
        else if(job == "Log")
        {
            Job = "Log";
            ATK = ATK + 3;
            MoveSpeed = MoveSpeed * 1.3f;
        }
    }
}

public class Character_Status : MonoBehaviour
{
    [SerializeField] Text StatusHP = null;
    [SerializeField] Text StatusATK = null;
    [SerializeField] Text StatusATKSpeed = null;
    [SerializeField] Text StatusMP = null;
    [SerializeField] Text StatusDEF = null;
    [SerializeField] Text StatusDodge = null;
    [SerializeField] Text StatusCritical = null;
    [SerializeField] Text StatusMoveSpeed = null;
    [SerializeField] Text StatusJob = null;
    [SerializeField] Text StatusHeredity = null;
    [SerializeField] GameObject StatusPanel;

    string PathData = "Data";
    bool IsStatus = false;
    private void Start()
    {
        string jsonFileName = "Data/Character_Status.json";
        string strFile = string.Format("{0}/{1}", Application.dataPath, jsonFileName);
        if (File.Exists(strFile))
        {}
        else
        {
            PlayerInfo status = new PlayerInfo();
            string jsonData = ObjectToJson(status);
            CreateJsonFile(string.Format("{0}/{1}", Application.dataPath, PathData), "Character_Status", jsonData);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            if(IsStatus)
            {
                StatusPanel.gameObject.SetActive(false);
                IsStatus = false;
            }
            else
            {
                StatusPanel.gameObject.SetActive(true);
                IsStatus = true;
            }
        }
    }

    void setStatusMenu()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");

        StatusHP.text = "HP = "+json.HP.ToString();
        StatusATK.text = "ATK = "+json.ATK.ToString();
        StatusATKSpeed.text = "ATKSpeed = "+json.ATKSpeed.ToString();
        StatusMP.text = "MP = "+json.MP.ToString();
        StatusDEF.text = "DEF = "+json.DEF.ToString();
        StatusDodge.text = "Dodge = "+json.Dodge.ToString();
        StatusCritical.text = "Critical = "+json.Critical.ToString();
        StatusMoveSpeed.text = "MoveSpeed = "+json.MoveSpeed.ToString();
        StatusJob.text = "Job = "+json.Job;
        StatusHeredity.text = json.Heredity;
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
