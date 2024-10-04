using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{

    public Room[,] map;
    // Left -> Right goes from low indices to high, Down -> Up goes from low to high

    public int numRooms;
    int roomsGenerated;

    // List of rooms that can be expanded.
    List<Tuple<int, int>> expandRooms = new List<Tuple<int, int>>();
    int playerCoords;
    public int seed;
    int[] wallNums = { -1, -1, -1, -1 };
    System.Random random;

    // Start with room that player is in and add one random room attached to it. Add these two rooms
    // to expandRooms, then repeatedly "expand" each room until no more rooms left. Finally, fill in all
    // gaps with flat walls.

    // "Expanding" a room means adding at least one room, branching off of it, adding these rooms to expandRooms,
    // then removing the original room from expandRooms.
    public void Generate()
    {
        if (numRooms <= 1)  // Only accept 2 or more rooms
        {
            Application.Quit();
        }

        random = new System.Random(seed);
        map = new Room[numRooms, numRooms];
        int secondRoomIndex = (int)(random.NextDouble() * 4);

        wallNums[secondRoomIndex] = (int)(random.NextDouble() * 2);
        playerCoords = numRooms / 2;
        map[playerCoords, playerCoords] = new Room(wallNums[0], wallNums[1], wallNums[2], wallNums[3]);
        expandRooms.Add(new Tuple<int, int>(playerCoords, playerCoords));

        // Add the second room. (Code assumes numRooms > 1)
        wallNums = new int[] { -1, -1, -1, -1 };
        switch (secondRoomIndex)    // 0, 1, 2, 3? Left, right, up, or down compared to room 1?
        {
            case 0:
                wallNums[1] = 0;
                map[playerCoords - 1, playerCoords] = new Room(wallNums[0], wallNums[1], wallNums[2], wallNums[3]);
                expandRooms.Add(new Tuple<int, int>(playerCoords - 1, playerCoords));
                break;
            case 1:
                wallNums[0] = 0;
                map[playerCoords + 1, playerCoords] = new Room(wallNums[0], wallNums[1], wallNums[2], wallNums[3]);
                expandRooms.Add(new Tuple<int, int>(playerCoords + 1, playerCoords));
                break;
            case 2:
                wallNums[2] = 0;
                map[playerCoords, playerCoords + 1] = new Room(wallNums[0], wallNums[1], wallNums[2], wallNums[3]);
                expandRooms.Add(new Tuple<int, int>(playerCoords, playerCoords + 1));
                break;
            case 3:
                wallNums[3] = 0;
                map[playerCoords, playerCoords - 1] = new Room(wallNums[0], wallNums[1], wallNums[2], wallNums[3]);
                expandRooms.Add(new Tuple<int, int>(playerCoords, playerCoords - 1));
                break;

        }
        roomsGenerated += 2;

        while (numRooms > roomsGenerated)
        {
            ExpandRooms();
        }

        foreach (Tuple<int, int> t in expandRooms)
        {
            Room r = map[t.Item1, t.Item2];
            if (r.left == -1)
            {
                r.left = 2;
            }
            if (r.right == -1)
            {
                r.right = 2;
            }
            if (r.up == -1)
            {
                r.up = 2;
            }
            if (r.down == -1)
            {
                r.down = 2;
            }
        }

        /*for (int c = 0; c < map.GetLength(0); c++)
        {
            for (int row = 0; row < map.GetLength(1); row++)
            {
                Room r = map[c, row];
                if (r != null)
                {
                    if (r.left == -1)
                    {
                        r.left = 2;
                    }
                    if (r.right == -1)
                    {
                        r.right = 2;
                    }
                    if (r.up == -1)
                    {
                        r.up = 2;
                    }
                    if (r.down == -1)
                    {
                        r.down = 2;
                    }
                    //Debug.Log("Room at " + c + ", " + row + ": (" +
                      //  r.left + ", " + r.right + ", " + r.up + ", " + r.down);
                }
            }
        }*/
            
    }

    // Code ended up being pretty messy, sorry. Should've maybe changed the Room struct to have an array
    // containing left, right, up, down.

    void ExpandRooms()
    {
        int currIndex = (int)(random.NextDouble() * expandRooms.Count);     // Random valid index in expandRooms list
        int c = expandRooms[currIndex].Item1;
        int r = expandRooms[currIndex].Item2;
        Room room = map[c, r];
        wallNums[0] = room.left;
        wallNums[1] = room.right;
        wallNums[2] = room.up;
        wallNums[3] = room.down;

        List<int> possDoors = new List<int>();
        int toC = c;
        int toR = r;                    // Indices in map that we want to open to
        for (int i = 0; i < wallNums.Length; i ++)
        {
            if (wallNums[i] == -1)
            {
                toC = c;
                toR = r;
                bool isValid = true;
                switch (i)
                {
                    case 0:
                        toC--;
                        if (toC < 0 || toC >= numRooms)
                            isValid = false;
                        else if (map[toC, toR] != null &&
                            map[toC, toR].right == 2)
                            isValid = false;
                        break;
                    case 1:
                        toC++;
                        if (toC < 0 || toC >= numRooms)
                            isValid = false;
                        else if (map[toC, toR] != null &&
                            map[toC, toR].left == 2)
                            isValid = false;
                        break;
                    case 2:
                        toR++;
                        if (toR < 0 || toR >= numRooms)
                            isValid = false;
                        else if (map[toC, toR] != null &&
                            map[toC, toR].down == 2)
                            isValid = false;
                        break;
                    case 3:
                        toR--;
                        if (toR < 0 || toR >= numRooms)
                            isValid = false;
                        else if (map[toC, toR] != null &&
                            map[toC, toR].up == 2)
                            isValid = false;
                        break;
                }
                if (isValid)
                {
                    possDoors.Add(i);
                }
                else
                {
                    wallNums[i] = 2;
                }
            }
        }
        toC = c;
        toR = r;
        Room toRoom = map[toC, toR];
        if (possDoors.Count > 0)
        {
            
            int firstDoor = possDoors[(int)(random.NextDouble() * possDoors.Count)];
            wallNums[firstDoor] = (int)(random.NextDouble() * 2);

            switch (firstDoor)
            {
                case 0:
                    toC--;
                    break;
                case 1:
                    toC++;
                    break;
                case 2:
                    toR++;
                    break;
                case 3:
                    toR--;
                    break;
            }

            toRoom = map[toC, toR];
            if (toRoom == null)
            {
                map[toC, toR] = new Room(-1, -1, -1, -1);
                toRoom = map[toC, toR];
                expandRooms.Add(new Tuple<int, int>(toC, toR));
                roomsGenerated++;
            }
            switch (firstDoor)
            {
                case 0:
                    toRoom.right = 0;
                    break;
                case 1:
                    toRoom.left = 0;
                    break;
                case 2:
                    toRoom.down = 0;
                    break;
                case 3:
                    toRoom.up = 0;
                    break;
            }
            possDoors.Remove(firstDoor);
        }
        foreach (int i in possDoors)
        {
            int coinFlip = (int)(random.NextDouble() * 2);
            if (coinFlip == 0)
            {
                wallNums[i] = 2;
            }
            else
            {
                toC = c;
                toR = r;
                switch (i)
                {
                    case 0:
                        toC--;
                        break;
                    case 1:
                        toC++;
                        break;
                    case 2:
                        toR++;
                        break;
                    case 3:
                        toR--;
                        break;
                }
                toRoom = map[toC, toR];
                if (toRoom == null && roomsGenerated < numRooms)
                {
                    map[toC, toR] = new Room(-1, -1, -1, -1);
                    toRoom = map[toC, toR];
                    expandRooms.Add(new Tuple<int, int>(toC, toR));
                    roomsGenerated++;
                }
                if (toRoom != null)
                {
                    wallNums[i] = (int)(random.NextDouble() * 2);
                    switch (i)
                    {
                        case 0:
                            toRoom.right = 0;
                            break;
                        case 1:
                            toRoom.left = 0;
                            break;
                        case 2:
                            toRoom.down = 0;
                            break;
                        case 3:
                            toRoom.up = 0;
                            break;
                    }
                }
            }
        }
        for (int i = 0; i < wallNums.Length; i ++)
        {
            if (wallNums[i] == -1)
                wallNums[i] = 2;
        }
        room.left = wallNums[0];
        room.right = wallNums[1];
        room.up = wallNums[2];
        room.down = wallNums[3];
        expandRooms.RemoveAt(currIndex);
    }
    
}
