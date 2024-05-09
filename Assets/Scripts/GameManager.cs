using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject collectibleParent;
    [SerializeField] private GameObject stackPoint;
    
    private int _score;
    private void OnEnable()
    {
        EventManager.Subscribe(EventList.GameStarted, OnGameStarted);
        EventManager.Subscribe(EventList.GameFailed, OnGameFailed);
        EventManager.Subscribe(EventList.GameWon, OnGameWon);
        EventManager.Subscribe(EventList.OnFinishLineCrossed,CalculateScore);
    }
    
    private void OnDisable()
    {
        EventManager.Unsubscribe(EventList.GameStarted, OnGameStarted);
        EventManager.Unsubscribe(EventList.GameFailed, OnGameFailed);
        EventManager.Unsubscribe(EventList.GameWon, OnGameWon);
        EventManager.Unsubscribe(EventList.OnFinishLineCrossed,CalculateScore);
    }
    
    
    void OnGameStarted()
    {
        Debug.Log("Game Started");
    }
    
    void OnGameFailed()
    {
        Debug.Log("Game Failed");
    }
    
    void OnGameWon()
    {
        Debug.Log("Game Won");
    }

    void CalculateScore()
    {
        StartCoroutine(StackCollectibles());
    }
    
    IEnumerator StackCollectibles()
    {
        foreach (Transform child in collectibleParent.transform)
        {
            child.transform.position = stackPoint.transform.position;
            stackPoint.transform.position += Vector3.up*1.5f;
            yield return new WaitForSeconds(0.50f);
           // Debug.Log("waited for 0.5 seconds");
        }
    }
    
}
