using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool dead = false;

    public float max_hp = 100.0f;
    public float hp = 100.0f;
    public float damage = 10.0f;
    public float attack_speed = 0.5f;

    public bool cooltime = true;

    public float knock_back = 2.0f;

    public float invincible_time = 1.0f;
    private bool invicible = false;
    private SpriteRenderer spriteRenderer;

    private PlayerPad controller;

    private Rigidbody2D rig2d;
    private Transform tr;
    private Vector2 move;

    public float jump_power = 15.0f;
    public float move_power = 10.0f;

    public GameObject jump_particle;

    private int jump_count;

    private PlatformEffector2D pe;

    public new Camera camera;
    private Vector2 mouse_cursor;

    public GameObject incidental;

    private GameObject weapon;
    private Transform w_tr;

    private bool attack_cool_time = false;

    private Quaternion sword_move;

    private Animator anim;

    string MostHighStatus;

    [SerializeField] private GameObject PlayerDiedPanel;

    [SerializeField] GameObject[] SkillPrefab;

    Vector2 MousePosition;
    Camera Camera;

    private Rigidbody2D myRigidBody;

    [SerializeField] private float speed;
    [SerializeField] Transform m_tfINT;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

        controller = new PlayerPad();

        rig2d = gameObject.GetComponent<Rigidbody2D>();
        tr = gameObject.GetComponent<Transform>();
        anim = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        weapon = gameObject.transform.GetChild(0).gameObject;
        w_tr = weapon.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            Jump();
            Move();
            ChangeDirection();
            Attack();
        }
        checkMostHighStatus();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            jump_count = 2;
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            pe = collision.gameObject.GetComponent<PlatformEffector2D>();
        }
        
        if (collision.gameObject.CompareTag("Enemy") && !invicible)
        {
            hp -= 100;
        
            if (hp <= 0)
            {
                invicible = true;
                anim.SetTrigger("Die");
                dead = true;
                Ignore_Monster_Attack(true);
                PlayerDied();
            }
            else
            {
                StartCoroutine("Invicible");
        
                Vector2 attacked_velocity = Vector2.zero;
                if (collision.gameObject.transform.position.x < tr.position.x)
                {
                    attacked_velocity = new Vector2(7f, 7f);
                }
                else
                {
                    attacked_velocity = new Vector2(-7f, 7f);
                }
        
                rig2d.AddForce(attacked_velocity, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            pe = null;

            if (collision.gameObject.CompareTag("Enemy"))
            {
                hp -= 10;
                Debug.Log("hit player");
            }
        }
    }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Platform") && pe != null)
    //     {
    //         anim.StopPlayback();
    //         pe.surfaceArc = 170;
    //         pe = null;
    //     }
    // }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (jump_count > 0)
            {
                if (jump_count == 1)
                {
                    Instantiate(jump_particle, tr.position, tr.rotation);
                }

                jump_count--;

                rig2d.velocity = Vector2.zero;

                Vector2 jumpVelocity = new Vector2(0, jump_power);
                rig2d.AddForce(jumpVelocity, ForceMode2D.Impulse);

                anim.SetTrigger("Jump");
            }
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector2 position = tr.position;

        position.x += horizontal * Time.deltaTime * move_power;

        tr.position = position;

        if (Input.GetKeyDown(KeyCode.S) && pe != null && pe.surfaceArc > 0)
        {
            pe.surfaceArc = 0;
        }

        anim.SetBool("Move", Convert.ToBoolean(horizontal));
    }

    private void ChangeDirection()
    {
        if (!attack_cool_time)
        {

            mouse_cursor = camera.ScreenToWorldPoint(Input.mousePosition);

            if (mouse_cursor.x > tr.position.x)
            {
                if(UseJsonFile().Heredity == "Gigantism")
                    tr.localScale = new Vector3(1.5f, 1.5f, 1);
                else if(UseJsonFile().Heredity == "Predisposition")
                    tr.localScale = new Vector3(0.5f, 0.5f, 1);
                else if(UseJsonFile().Heredity == "Schizophrenia")
                {
                    tr.localScale = new Vector3(-1, 1, 1);
                }
                else tr.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                if (UseJsonFile().Heredity == "Gigantism")
                    tr.localScale = new Vector3(-1.5f, 1.5f, 1);
                else if (UseJsonFile().Heredity == "Predisposition")
                    tr.localScale = new Vector3(-0.5f, 0.5f, 1);
                else if (UseJsonFile().Heredity == "Schizophrenia")
                {
                    tr.localScale = new Vector3(1, 1, 1);
                }
                else tr.localScale = new Vector3(-1, 1, 1);
            }

            float height = mouse_cursor.y - tr.position.y;
            float width = Math.Abs(mouse_cursor.x - tr.position.x);

            float angle = Convert.ToSingle((Math.Atan2(height, width) * 180 / Math.PI - 45) * tr.localScale.x);

            w_tr.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !attack_cool_time)
        {
            float effect_z = w_tr.rotation.eulerAngles.z + tr.localScale.x * 45f;

            GameObject incident = Instantiate(incidental,
                weapon.transform.gameObject.GetComponent<Transform>().position,
                Quaternion.Euler(0, 0, effect_z));

            Transform e_tr = incident.GetComponent<Transform>();
            e_tr.localScale = new Vector3(tr.localScale.x, 1, 1);
            e_tr.position = new Vector3(e_tr.position.x, e_tr.position.y, -4f);

            Destroy(incident, 0.1f);
            attack_cool_time = true;
            Invoke("Attack_Cool", attack_speed);

            float z = w_tr.localRotation.eulerAngles.z;


            if (225 <= z && z <= 315)
            {
                z = Degree_Calc(z, -90);
            }
            else
            {
                z = Degree_Calc(z, 90);
            }


            sword_move = Quaternion.Euler(0, 0, z);
        }
        else if (attack_cool_time)
        {
            w_tr.localRotation = Quaternion.Lerp(w_tr.rotation, sword_move, 9 * Time.deltaTime / attack_speed);
        }
    }

    private void Attack_Cool()
    {
        attack_cool_time = false;
    }

    private float Degree_Calc(float a, float b)
    {
        float x = a - b;

        if (x < 0)
        {
            x += 360;
        }
        else if (x > 360)
        {
            x -= 360;
        }

        return x;
    }

    private IEnumerator Invicible()
    {
        invicible = true;

        int count_time = 0;

        Ignore_Monster_Attack(true);

        while (count_time < 10)
        {
            if (count_time % 2 == 0)
            {
                spriteRenderer.color = new Color32(255, 255, 255, 90);
            }
            else
            {
                spriteRenderer.color = new Color32(255, 255, 255, 180);
            }

            yield return new WaitForSeconds(invincible_time / 5);

            count_time++;
        }
        if (!dead)
        {
            invicible = false;

            Ignore_Monster_Attack(false);
        }

        spriteRenderer.color = new Color32(255, 255, 255, 255);

        yield return null;
    }

    private void Ignore_Monster_Attack(bool ignore)
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), ignore);
    }

    void PlayerDied()
    {
        PlayerDiedPanel.gameObject.SetActive(true);
    }

    public void ReviveSettings()
    {
        invicible = false;
        dead = false;
        Ignore_Monster_Attack(false);
    }

    private void Schizophrenia()
    {
        Debug.Log("불렀어");

        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < 10; j++)
                tr.localScale = new Vector3(1f * -1f, 1, 1);
        }
    }

    PlayerInfo UseJsonFile()
    {
        var json = LoadJsonFile<PlayerInfo>(Application.dataPath + "/Data", "Character_Status");
        return json;
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
}
