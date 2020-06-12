using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.IO;
using System.Text;

public class MonsterInfo : MonoBehaviour
{
    public GameObject[] items;
    public float[] percentage;

    public float hp = 100.0f;
    public float damage = 100.0f;

    public GameObject dead_effect;

    private GameObject player;
    private PlayerController pc;
    private Rigidbody2D rig2d;
    private Animator anim;

    public AIPath aI;

    string PathData = "Data";

    // Start is called before the first frame update
    void Start()
    {
        rig2d = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<PlayerController>();
        anim = gameObject.GetComponent<Animator>();
        aI = gameObject.GetComponent<AIPath>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > gameObject.transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            hp -= pc.damage;

            if(hp <= 0)
            {
                Die();
                return;
            }

            Vector2 attacked_velocity = Vector2.zero;
            if (collision.gameObject.transform.position.x < gameObject.transform.position.x)
            {
                attacked_velocity = new Vector2(pc.knock_back, pc.knock_back);
            }
            else
            {
                attacked_velocity = new Vector2(-pc.knock_back, pc.knock_back);
            }

            StartCoroutine("Stop_Astar");

            rig2d.AddForce(attacked_velocity, ForceMode2D.Impulse);

            Debug.Log(attacked_velocity);
        }
        else if (collision.CompareTag("INT"))
        {
            hp -= pc.damage;

            if (hp <= 0)
            {
                Die();
                return;
            }

            Vector2 Skill_velocity = Vector2.zero;
            if (collision.gameObject.transform.position.x < gameObject.transform.position.x)
            {
                Skill_velocity = new Vector2(pc.knock_back, pc.knock_back);
            }
            else
            {
                Skill_velocity = new Vector2(-pc.knock_back, pc.knock_back);
            }

            StartCoroutine("Stop_Astar");

            rig2d.AddForce(Skill_velocity, ForceMode2D.Impulse);

            Debug.Log(Skill_velocity);
        }
        else if (collision.CompareTag("STR"))
        {
            hp -= pc.damage;

            if (hp <= 0)
            {
                Die();
                return;
            }

            Vector2 Skill_velocity = Vector2.zero;
            if (collision.gameObject.transform.position.x < gameObject.transform.position.x)
            {
                Skill_velocity = new Vector2(pc.knock_back, pc.knock_back);
            }
            else
            {
                Skill_velocity = new Vector2(-pc.knock_back, pc.knock_back);
            }

            StartCoroutine("Stop_Astar");

            rig2d.AddForce(Skill_velocity, ForceMode2D.Impulse);

            Debug.Log(Skill_velocity);
        }
    }

    private IEnumerator Stop_Astar()
    {
        aI.canMove = false;

        yield return new WaitForSeconds(0.1f);

        aI.canMove = true;

        yield return null;
    }

    private void Die()
    {
        gameObject.transform.tag = "Untagged";
        //Instantiate(dead_effect, gameObject.transform);
        anim.SetTrigger("Die");
        DropItem();
        Destroy(gameObject, 0.5f);
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        json.RecentEXP += 10;
        string jsonData = ObjectToJson(json);
        CreateJsonFile(string.Format("{0}/{1}", Application.dataPath, PathData), "Character_Status", jsonData);
    }

    private void DropItem()
    {

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
