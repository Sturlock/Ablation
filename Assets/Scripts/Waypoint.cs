using System;
using UnityEngine;

[Serializable] public class Waypoint
{
    public float raduis;
    public Transform position;

    public Waypoint()
    {

    }

    public Waypoint(Waypoint _waypoint)
    {
        raduis = _waypoint.raduis;
        position = _waypoint.position;
    }
}

