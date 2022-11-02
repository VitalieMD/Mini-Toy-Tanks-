using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalEventManager : MonoBehaviour
{
    public static Action OnEnemyKilled;
    public static Action OnPlayer1Killed;
    public static Action OnPlayer2Killed;


    public static void SendEnemyKilled()
    {
        if (OnEnemyKilled != null) OnEnemyKilled.Invoke();
    }

        public static void SendPlayer1Killed()
    {
        if (OnPlayer1Killed != null) OnPlayer1Killed.Invoke();
    }

        public static void SendPlayer2Killed()
    {
        if (OnPlayer2Killed != null) OnPlayer2Killed.Invoke();
    }
}


