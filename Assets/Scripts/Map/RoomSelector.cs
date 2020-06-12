using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSelector : MonoBehaviour
{
    public GameObject[] B;
    public GameObject[] BR;
    public GameObject[] L;
    public GameObject[] LB;
    public GameObject[] LBR;
    public GameObject[] LR;
    public GameObject[] R;
    public GameObject[] T;
    public GameObject[] TB;
    public GameObject[] TBR;
    public GameObject[] TL;
    public GameObject[] TLB;
    public GameObject[] TLBR;
    public GameObject[] TLR;
    public GameObject[] TR;

    public GameObject boss_room;

    private static int a = 0;

    public void Create_Room(Room[,] rooms)
    {
		List<GameObject> room_data = new List<GameObject> ();

        foreach (Room room in rooms)
        {
            if(room == null)
            {
                continue;
            }
            if (room.boss)
            {
                Instantiate(boss_room, new Vector3(room.gridPos.x * 100, room.gridPos.y * 100, -5), Quaternion.Euler(0, 0, 0));
                return;
            }

			GameObject[] _room;

			if (room.doorTop)
			{
				if (room.doorBot)
				{
					if (room.doorRight)
					{
						if (room.doorLeft)
						{
							_room = TLBR;
						}
						else
						{
							_room = TBR;
						}
					}
					else if (room.doorLeft)
					{
						_room = TLB;
					}
					else
					{
						_room = TB;
					}
				}
				else
				{
					if (room.doorRight)
					{
						if (room.doorLeft)
						{
							_room = TLR;
						}
						else
						{
							_room = TR;
						}
					}
					else if (room.doorLeft)
					{
						_room = TL;
					}
					else
					{
						_room = T;
					}
				}
			}
			else if (room.doorBot)
			{
				if (room.doorRight)
				{
					if (room.doorLeft)
					{
						_room = LBR;
					}
					else
					{
						_room = BR;
					}
				}
				else if (room.doorLeft)
				{
					_room = LB;
				}
				else
				{
					_room = B;
				}
			}
			else if (room.doorRight)
			{
				if (room.doorLeft)
				{
					_room = LR;
				}
				else
				{
					_room = R;
				}
			}
			else
			{
				_room = L;
			}

			GameObject created_room = Instantiate(_room[(int)Random.Range(0, 3)], new Vector3(room.gridPos.x * 100, room.gridPos.y * 100, 20), Quaternion.Euler(0, 0, 0));
			created_room.GetComponent<RoomInfo>().gridPos = room.gridPos;
			created_room.GetComponent<RoomInfo>().Clear_Room();
			room_data.Add(created_room);
		}

		GameObject.Find("PlayerManager").GetComponent<PlayerManager>().Init_Rooms(room_data);

	}
}
