using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TextEvent : MonoBehaviour
{
    public TextMeshProUGUI player1Text;
    public TextMeshProUGUI player2Text;
    TankBaseClass tankBaseClass1;
    TankBaseClass tankBaseClass2;

    void Start()
    {
        tankBaseClass1 = GameObject.FindGameObjectWithTag("Player").GetComponent<TankBaseClass>();
        tankBaseClass2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<TankBaseClass>();
        GlobalEventManager.OnPlayer1Killed += Player1Text;
        GlobalEventManager.OnPlayer2Killed += Player2Text;
    }

    void OnDestroy()
    {
        GlobalEventManager.OnPlayer1Killed -= Player1Text;
        GlobalEventManager.OnPlayer2Killed -= Player2Text;
    }

    void Player1Text()
    {
        player1Text.text = "" + tankBaseClass1.dieCounter;
    }

    void Player2Text()
    {
        player2Text.text = "" + tankBaseClass2.dieCounter;
    }
}
