using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
   public static int Score { get; private set; }
   private void OnEnable()
   {
      EventManager.Subscribe(EventList.CalculateScore, CalculateResult);
   }

   private void OnDisable()
   {
      EventManager.Unsubscribe(EventList.CalculateScore, CalculateResult);
   }
   
   void CalculateResult(float value)
   {
      Score += (int) value;
   }
}
