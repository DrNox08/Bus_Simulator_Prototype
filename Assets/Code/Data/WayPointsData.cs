using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class WayPointsData
{
    [SerializeField] List<Transform> points = new List<Transform>();
    
    public Vector3[] GetWaypoints(float yValue)
    {
        List<Vector3> converted = new List<Vector3>();
        foreach (Transform t in points)
        {
            Vector3 pos = t.position;
            pos.y = yValue;
            converted.Add(pos);
        }

        return converted.ToArray();
    }
}
