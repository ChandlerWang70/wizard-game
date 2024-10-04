using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRooms : MonoBehaviour
{

    public RoomGenerator rG;
    public GameObject wall;
    public GameObject doorWall;
    public Vector3 playerPos;
    int playerCoords;
    Vector3 currPos;
    public GameObject[] props;
    List<Vector3> propSpots;
    System.Random random;
    int maxProps = 4;
    public GameObject enemySpawner;

    public void Start()
    {
        propSpots = new List<Vector3>();
        random = new System.Random(rG.seed);

        for (int i = 1; i < 5; i++)
        {
            for (int j = 1; j < 5; j++)
            {
                propSpots.Add(new Vector3(3f + 3 * i, 0, 3f + 3 * j));
            }
        }

        rG.Generate();
        playerCoords = rG.numRooms / 2;

        //rG.map[playerCoords + 2, playerCoords] = new Room(1, 2, 1, 2);

        for (int c = 0; c < rG.map.GetLength(0); c++)
        {
            for (int r = 0; r < rG.map.GetLength(1); r++)
            {
                if (rG.map[c, r] != null)
                {
                    Debug.Log("Room at " + c + ", " + r + ": (" +
                        rG.map[c, r].left + ", " + rG.map[c, r].right + ", " + rG.map[c, r].up + ", " + rG.map[c, r].down);
                    currPos = playerPos;
                    currPos.x += c * 20 - playerCoords * 20;
                    currPos.z += r * 20 - playerCoords * 20;
                    BuildRoom(c, r);
                }
            }
        }

    }

    void BuildRoom(int c, int r)
    {
        Room room = rG.map[c, r];
        float[] wallNums = { room.left, room.right, room.up, room.down };

        List<Vector3> tempSpots = new List<Vector3>(propSpots);
        Vector3 propPos = currPos + new Vector3(-10f, 0, -10f);


        for (int i = 0; i < 3; i++)
        {
            if ((int)(random.NextDouble() * 2) > 0)
            {
                int p = (int)(random.NextDouble() * tempSpots.Count);
                Instantiate(enemySpawner, propPos + tempSpots[p] + new Vector3(0, 1f, 0), Quaternion.identity);
                tempSpots.RemoveAt(p);
            }
        }

        for (int i = 0; i < maxProps; i++)
        {
            if ((int)(random.NextDouble() * 4) > 0)     //75%
            {
                int p = (int)(random.NextDouble() * tempSpots.Count);
                GameObject tempOb = props[(int)(random.NextDouble() * props.Length)];
                Instantiate(tempOb, propPos + tempSpots[p], Quaternion.identity);
                tempSpots.RemoveAt(p);
            }
        }

        for (int i = 0; i < wallNums.Length; i++)
        {
            Vector3 tempPos = currPos;
            Quaternion tempRot = Quaternion.identity;

            switch (i)
            {
                case 0:
                    tempPos.x -= 10f;
                    tempRot = Quaternion.Euler(0, 90, 0);
                    break;
                case 1:
                    tempPos.x += 10f;
                    tempRot = Quaternion.Euler(0, 90, 0);
                    break;
                case 2:
                    tempPos.z += 10f;
                    break;
                case 3:
                    tempPos.z -= 10f;
                    break;
            }
            switch (wallNums[i])
            {
                case -2:
                    break;
                case -1:
                    break;
                case 1:
                    Instantiate(doorWall, tempPos, doorWall.transform.rotation * tempRot);
                    break;
                case 2:
                    Instantiate(wall, tempPos, doorWall.transform.rotation * tempRot);
                    break;

            }
        }
    }


}
