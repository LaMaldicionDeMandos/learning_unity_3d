using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Zone
{
    public ZoneType type;
    public int width;
    public int height;
    public int x;
    public int y;

    public bool Belongs(int x, int y) {
        return x >= this.x && x < this.x + width
        && y >= this.y && y < this.y + height;
    }
}
