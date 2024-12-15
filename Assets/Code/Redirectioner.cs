using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redirectioner : MonoBehaviour
{
    [SerializeField] Transform wayPoint;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDrivable player)) player.SetNextWaypoint(wayPoint);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, wayPoint.position); 
    }
}
