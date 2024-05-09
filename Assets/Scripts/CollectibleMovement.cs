using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleMovement : MonoBehaviour
{
   public enum CollectibleType
   {
      Cow,
      Sheep,
   }
   
   [SerializeField] private GameObject cow;
   [SerializeField] private GameObject sheep;

   public CollectibleType _currentType;
   private Transform _currentLeadTransform;
   private float _currentVelocity=0f;
   private float _smoothTime=0.2f;

   private void Start()
   {
      switch (_currentType)
      {
         case CollectibleType.Cow:
            cow.gameObject.SetActive(true);
            sheep.gameObject.SetActive(false);
            break;
         case CollectibleType.Sheep:
            cow.gameObject.SetActive(false);
            sheep.gameObject.SetActive(true);
            break;
      }
   }

   private void Update()
   {
      if (!_currentLeadTransform)
         return;
      else
      {
         transform.position=new Vector3(Mathf.SmoothDamp(transform.position.x,_currentLeadTransform.position.x,ref _currentVelocity,_smoothTime),
            transform.position.y,transform.position.z);
      }
   }

   private void OnEnable()
   {
      EventManager.Subscribe(EventList.OnFinishLineCrossed, StopFollowing);
   }

   private void OnDisable()
   {
      EventManager.Unsubscribe(EventList.OnFinishLineCrossed, StopFollowing);
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
         case "Gate":
            ChangeMesh();
            break;
      }
   }

   void StopFollowing()
   {
      _currentLeadTransform = null;
      transform.GetComponent<BoxCollider>().isTrigger = true;
   }
   public void SetLeadTransform(Transform leadTransform)
   {
      _currentLeadTransform = leadTransform;
   }

   
   void ChangeMesh()
   {
      switch (_currentType)
      {
         case CollectibleType.Cow:
            _currentType = CollectibleType.Sheep;
            cow.gameObject.SetActive(false);
            sheep.gameObject.SetActive(true);
            break;
         case CollectibleType.Sheep:
            _currentType = CollectibleType.Cow;
            cow.gameObject.SetActive(true);
            sheep.gameObject.SetActive(false);
            break;
      }
   }
}
