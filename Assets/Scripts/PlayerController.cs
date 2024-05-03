using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float dragSensitivity;

    [SerializeField] private GameObject stackPoint;
    [SerializeField] private GameObject stackParent;


    private List<GameObject> _stackList = new List<GameObject>();
    private float _dragLimit = 4.2f;
    private float _horizontalOffset;
    private float _givenSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        _givenSpeed = forwardSpeed;
        forwardSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.position += Vector3.forward * (forwardSpeed * Time.deltaTime);
    }


    private void OnEnable()
    {
        EventManager.Subscribe(EventList.GameStarted, StartMovement);
        EventManager.Subscribe(EventList.OnHorizontalDrag,HorizontalMovement);
        EventManager.Subscribe(EventList.OnCollectiblePickup, AddStack);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(EventList.GameStarted, StartMovement);
        EventManager.Unsubscribe(EventList.OnHorizontalDrag,HorizontalMovement);
        EventManager.Unsubscribe(EventList.OnCollectiblePickup, AddStack);
    }
    
    private void StartMovement()
    {
        forwardSpeed = _givenSpeed;
    }
    
    private void HorizontalMovement(float horizontal)
    {
        transform.position += Vector3.right * (horizontal * dragSensitivity * Time.deltaTime);
        var playerPosition = transform.position;
        playerPosition.x = Mathf.Clamp(transform.position.x,-_dragLimit,_dragLimit);
        transform.position = playerPosition;
    }

    private void AddStack(GameObject collectible)
    {
        if (_stackList.Contains(collectible)) 
            return;
        collectible.transform.tag = "Collected";
        collectible.transform.GetComponent<BoxCollider>().isTrigger = false;
        _stackList.Add(collectible);
        collectible.transform.SetParent(stackParent.transform, true);
        collectible.transform.localPosition = stackPoint.transform.localPosition;
        stackPoint.transform.localPosition += Vector3.forward * 2;

        if (_stackList.Count==1)
        {
            _stackList[0].GetComponent<CollectibleMovement>().SetLeadTransform(transform);
        }
        else if (_stackList.Count>1)
        {
            _stackList[^1].GetComponent<CollectibleMovement>().SetLeadTransform(_stackList[^2].transform);
        }
        {
            
        }
    }
}
