using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RechargeBar : MonoBehaviour
{
    [SerializeField] private Image _rechargeAmmoBarFiller;
    //[SerializeField] private Gun _gunScript;
    void Update()
    {
        //_rechargeAmmoBarFiller.fillAmount = _gunScript.FireTimer / 4;
    }
}
