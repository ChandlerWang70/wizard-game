using System.Collections.Generic;

public class Room
{

    /* For each direction (left, right, down, up): 0 = no wall, 1 = 
     * wall with door, 2 = wall with no door
     */

    public int left;
    public int right;
    public int up;
    public int down;


    public Room(int left, int right, int down, int up)
    {
        this.left = left;
        this.right = right;
        this.down = down;
        this.up = up;
    }
}
