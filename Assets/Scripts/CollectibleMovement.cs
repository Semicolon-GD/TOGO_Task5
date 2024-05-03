using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleMovement : MonoBehaviour
{
   private Transform _currentLeadTransform;
   private float _currentVelocity=0f;
   private float _smoothTime=0.2f;

   private void Update()
   {
      if (!_currentLeadTransform)
         return;
      else
      {
         /*transform.position=Vector3.Lerp(transform.position,_currentLeadTransform.position,Time.deltaTime);*/
         transform.position=new Vector3(Mathf.SmoothDamp(transform.position.x,_currentLeadTransform.position.x,ref _currentVelocity,_smoothTime),
            transform.position.y,transform.position.z);
      }
   }
   
   public void SetLeadTransform(Transform leadTransform)
   {
      _currentLeadTransform = leadTransform;
   }


   private void OnTriggerEnter(Collider other)
   {
      switch (other.tag)
      {
         case "Player":
            EventManager.Trigger(EventList.OnCollectiblePickup,this.gameObject);
            break;
         case "Collected":
            EventManager.Trigger(EventList.OnCollectiblePickup,this.gameObject);
            break;
         case "Obstacle":
            EventManager.Trigger(EventList.OnObstacleHit);
            break;
      }
   }
}
