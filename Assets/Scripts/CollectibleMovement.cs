using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleMovement : MonoBehaviour
{
   public enum CollectibleType
   {
      Cube,
      Sphere
   }
   
   [SerializeField] private MeshFilter meshFilter;
   [SerializeField] private Mesh cube;
   [SerializeField] private Mesh sphere;

   public CollectibleType _currentType;
   private Transform _currentLeadTransform;
   private float _currentVelocity=0f;
   private float _smoothTime=0.2f;

   private void Start()
   {
      _currentType = CollectibleType.Cube;
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
      if (meshFilter.sharedMesh==cube)
      {
         meshFilter.sharedMesh = sphere;
         _currentType= CollectibleType.Sphere;
      }
      else if (meshFilter.sharedMesh==sphere)
      {
         meshFilter.sharedMesh = cube;
         _currentType= CollectibleType.Cube;
      }
   }
}
