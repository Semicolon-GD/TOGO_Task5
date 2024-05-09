using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject collectibleParent;
    [SerializeField] private GameObject stackPoint;
    [SerializeField] private GameObject inputPanel;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TMPro.TextMeshProUGUI resultText;
    [SerializeField] private TMPro.TextMeshProUGUI resultScore; 
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    

    private int _amount=0;
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

    private void Start()
    {
        tutorialPanel.SetActive(true);
    }

    void OnGameStarted()
    {
        tutorialPanel.SetActive(false);
    }
    
    void OnGameFailed()
    {
        resultPanel.SetActive(true);
        inputPanel.SetActive(false);
        resultScore.gameObject.SetActive(false);
        resultPanel.GetComponent<Image>().color = Color.red;
        resultText.text = "Game Over";
       
    }
    
    void OnGameWon()
    {
        var color = resultPanel.GetComponent<Image>().color;
        color = Color.green;
        color.a = 0.5f;
        resultPanel.GetComponent<Image>().color = color;
        resultPanel.SetActive(true);
        inputPanel.SetActive(false);
       
        
    }

    void CalculateScore()
    {
        StartCoroutine(StackCollectibles());
    }

    

    IEnumerator StackCollectibles()
    {
        foreach (Transform child in collectibleParent.transform)
        {
            switch (child.GetComponent<CollectibleMovement>()._currentType)
            {
                case CollectibleMovement.CollectibleType.Cow:
                    EventManager.Trigger(EventList.CalculateScore, 10f);
                    break;
                case CollectibleMovement.CollectibleType.Sheep:
                    EventManager.Trigger(EventList.CalculateScore, 5f);
                    break;
                default:
                    break;
            }

            StartCoroutine(WriteScore());
            child.transform.position = stackPoint.transform.position;
            stackPoint.transform.position += Vector3.up*1.5f;
            yield return new WaitForSeconds(0.35f);
           // Debug.Log("waited for 0.5 seconds");
        }
    }

    private IEnumerator WriteScore()
    {
        while (_amount<ScoreSystem.Score)
        {
           
            _amount++;
            resultScore.text = _amount.ToString();
            yield return new WaitForSeconds(0.05f);
        }
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
