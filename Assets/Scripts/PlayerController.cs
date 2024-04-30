using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float dragSensitivity;


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
        transform.position += Vector3.forward * (forwardSpeed * Time.deltaTime);
    }


    private void OnEnable()
    {
        EventManager.Subscribe(EventList.GameStarted, StartMovement);
        InputManager.OnDragging+= HorizontalMovement;
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(EventList.GameStarted, StartMovement);
        InputManager.OnDragging-= HorizontalMovement;
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
}
