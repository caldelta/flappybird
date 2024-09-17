using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BlockCollision
{
    public Vector3 Start { get; set; }
    public Vector3 End { get; set; }
    public Vector3 Point { get; set; }

    private Vector3 m_pointResult { get; set; }

    public bool IsCollide
    {
        get
        {
            m_pointResult = Vector3Extenstion.ProjectPointOnLineSegment(Start, End, Point);
            return (Point - m_pointResult).magnitude <= BlockConst.COLLIDE_DISTANCE;
        }
    }
}
