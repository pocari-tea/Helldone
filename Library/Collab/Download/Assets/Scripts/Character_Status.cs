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
    public int EXP; // 레벨업 경험치
    public int RecentEXP; // 현재 경험치
    public int recentHP; // 현재 체력
    public int HP; // 체력
    public int ATK; // 공격력
    public int STR; // 힘
    public int DEX; // 민첩
    public int INT; // 지능
    public float ATKSpeed; // 공격속도
    public int MP; // 기력
    public int recentMP; // 현재 기력
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
        EXP = 60; // 레벨업 경험치
        RecentEXP = 0; // 현재 경험치
        HP = 100; // 체력
        recentHP = 100; // 현재 체력
        ATK = 10; // 공격력
        STR = 10; // 힘
        DEX = 10; // 민첩
        INT = 10; // 지능
        ATKSpeed = 1.0f; // 공격속도
        MP = 10; // 기력
        recentMP = 10; // 현재 기력
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
    [SerializeField] Text StatusSTR = null;
    [SerializeField] Text StatusDEX = null;
    [SerializeField] Text StatusINT = null;
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
        if (File.Exists(strFile)) { }
        else
        {
            PlayerInfo status = new PlayerInfo();
            string jsonData = ObjectToJson(status);
            CreateJsonFile(string.Format("{0}/{1}", Application.dataPath, PathData), "Character_Status", jsonData);
        }
        
    }

    public void STRUp()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        json.STR++;
        string jsonData = ObjectToJson(json);

        CreateJsonFile(string.Format("{0}/{1}", Application.dataPath, PathData), "Character_Status", jsonData);
    }

    public void DEXUp()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        json.DEX++;
        string jsonData = ObjectToJson(json);

        CreateJsonFile(string.Format("{0}/{1}", Application.dataPath, PathData), "Character_Status", jsonData);
    }

    public void INTUp()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        json.INT++;
        string jsonData = ObjectToJson(json);
        CreateJsonFile(string.Format("{0}/{1}", Application.dataPath, PathData), "Character_Status", jsonData);
    }


    private void Update()
    {
        setStatusMenu();

        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        string jsonData = ObjectToJson(json);
        if(json.RecentEXP >= json.EXP)
        {
            json.RecentEXP = json.RecentEXP % json.EXP;
            json.LV++;
            CreateJsonFile(string.Format("{0}/{1}", Application.dataPath, PathData), "Character_Status", jsonData);
        }
        

        if (Input.GetKeyDown(KeyCode.K))
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

        StatusHP.text = string.Format("체력 = {0}/{1}", json.HP, json.recentHP);
        StatusATK.text = "공격력 = " + json.ATK.ToString();
        StatusSTR.text = "힘 = " + json.STR.ToString();
        StatusDEX.text = "민첩 = " + json.DEX.ToString();
        StatusINT.text = "지능 = " + json.INT.ToString();
        StatusATKSpeed.text = "공격 속도 = " + json.ATKSpeed.ToString();
        StatusMP.text = string.Format("기력 = {0}/{1}", json.MP, json.recentMP);
        StatusDEF.text = "방어력 = " + json.DEF.ToString();
        StatusDodge.text = "회피률 = " + json.Dodge.ToString();
        StatusCritical.text = "치명타 확률 = " + json.Critical.ToString();
        StatusMoveSpeed.text = "이동속도 = " + json.MoveSpeed.ToString();
        StatusJob.text = "직업 = " + json.Job;
        StatusHeredity.text = json.Heredity;
    }

    public void HPFull()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        json.recentHP = json.HP;
        string jsonData = ObjectToJson(json);
        CreateJsonFile(string.Format("{0}/{1}", Application.dataPath, PathData), "Character_Status", jsonData);
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
