using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWaypoints : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
