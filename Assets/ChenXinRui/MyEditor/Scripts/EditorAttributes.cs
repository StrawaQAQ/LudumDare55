using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DrawGizmosType { rectangle = 0, circle }
[Serializable]
public class GizmosSetting
{

    public bool editorMode;
    public bool openGizmos;
    public DrawGizmosType GizmosType;
    public Transform point;
    public float radius;
    public Vector2 size, offSet;
    public Color gizmosColor;
    public Vector2 gizmosCenter { get { return (Vector2)point.position + offSet; } }

    public void AutoRange(Vector2Int xRange,Vector2Int yRange)
    {
        point.position = Vector3.zero;
        int sizeX = xRange.y - xRange.x;
        int sizeY = yRange.y - yRange.x;
        size = new Vector2(sizeX, sizeY);
        offSet= new Vector2((xRange.y + xRange.x)/2f, (yRange.y + yRange.x)/2f);
    }
}
