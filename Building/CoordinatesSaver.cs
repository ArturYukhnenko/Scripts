using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class CoordinatesSaver
{
    public string type;
    public float pos_x, pos_y, pos_z, rot_x, rot_y, rot_z, rot_w;

    public CoordinatesSaver(string type, float x, float y, float z, float rx, float ry, float rz, float rw)
    {
        this.type = type;
        pos_x = x;
        pos_y = y;
        pos_z = z;
        rot_x = rx;
        rot_y = ry;
        rot_z = rz;
        rot_w = rw;
    }
    
}
