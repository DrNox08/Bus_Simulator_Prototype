using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDrivable 
{
    void SetNextWaypoint(Transform nextDestionation);

    void StopAgent(bool value);

    
}
