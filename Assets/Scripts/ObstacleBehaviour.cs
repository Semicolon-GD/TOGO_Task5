using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        switch (other.tag)
        {
            case "Player":
            case "Collected":
                EventManager.Trigger(EventList.OnObstacleHit,other.gameObject);
                break;
        }
    }
}
