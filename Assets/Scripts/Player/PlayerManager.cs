using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerManager : MonoBehaviour
{
    public List<GameObject> rooms;
    public GameObject current_room;
    public Vector2 current_pos;
    public GameObject player;
    public GameObject A_;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        A_ = GameObject.Find("A_");
    }

    public void Init_Rooms(List<GameObject> rooms)
    {
        this.rooms = rooms;
        current_room = Find_Room_with_Pos(new Vector2(0, 0));
        Vector3 grid_pos = current_room.GetComponent<RoomInfo>().gridPos;
        current_pos = new Vector2(grid_pos.x, grid_pos.y);
    }

    public void Room_Move(bool top, bool right, bool bot, bool left)
    {
        string direction = "";

        if (top) {
            current_pos.y += 1f;
            direction = "B";
        }
        else if (right) {
            current_pos.x += 1f;
            direction = "L";
        }
        else if (bot) {
            current_pos.y -= 1f;
            direction = "T";
        }
        else if(left) {
            current_pos.x -= 1f;
            direction = "R";
        }

        current_room = Find_Room_with_Pos(current_pos);

        Debug.Log(current_pos);
        if ((int)current_pos.x == current_pos.x && (int)current_pos.y == current_pos.y)
        {


            Vector3 dir = current_room.transform.Find(direction).GetComponent<Transform>().position;

            player.transform.position = new Vector3(dir.x, dir.y, -5);
        }

    }

    private GameObject Find_Room_with_Pos(Vector2 pos)
    {
        foreach(GameObject room in rooms)
        {
            RoomInfo r = room.GetComponent<RoomInfo>();
            if (r.gridPos.x == pos.x)
            {
                if(r.gridPos.y == pos.y)
                {
                    return room;
                }
            }
        }

        return null;
    }
}
