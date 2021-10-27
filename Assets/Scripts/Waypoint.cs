using System;
using UnityEngine;

[Serializable] public class Waypoint
{
    public float radius;
    public Vector3 position;

    public Waypoint()
    {

    }

    public Waypoint(Waypoint _waypoint)
    {
        radius = _waypoint.radius;
        position = _waypoint.position;
    }
}



