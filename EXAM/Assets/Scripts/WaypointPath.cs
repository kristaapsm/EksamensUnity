using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
   public Transform GetWaypont(int waypointIndex)
    {
        return transform.GetChild(waypointIndex);
    }

    public int GetNextWaypointIndex (int currentWPI)
    {
        int nextWPI = currentWPI + 1;
        
        if (nextWPI == transform.childCount)
        {
            nextWPI = 0;
        }
        return nextWPI;
    }
}