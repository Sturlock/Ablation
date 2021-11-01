using System;
using UnityEngine;

[Serializable] public class Waypoint
{
    public float radius;
    public Vector3 position;

    public Waypoint()
    {

    }
  
    /// <summary>
    /// Create copy from other wp
    /// </summary>
    /// <param name="_waypoint"></param>
    public Waypoint(Waypoint _waypoint)
    {
        radius = _waypoint.radius;
        position = _waypoint.position;
    }
}