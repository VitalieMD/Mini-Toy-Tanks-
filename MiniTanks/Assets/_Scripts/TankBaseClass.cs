using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TankBaseClass : MonoBehaviour
{
    [Header("Base Health Stats")]
    [SerializeField] protected float Health;
    [SerializeField] protected float Currenthealth;
    public event Action<float> HealthChanged;
    [SerializeField] protected bool isShieldActive = false;

    [Header("Base Movement Stats")]
    [SerializeField] protected float TankSpeed;
    [SerializeField] protected AudioSource MovementAudio;
    [SerializeField] protected AudioClip TankMovementSound;
    [SerializeField] protected AudioClip TankIdleSound;
    [Header("Base Shooting Stats")]
    [SerializeField] protected Rigidbody BulletPrefab;
    Projectile enemyProjectile;
    [SerializeField] protected Projectile thisProjectile;
    public float bulletSpeed = 20;
    [SerializeField] protected Transform FirePoint;
    public int dieCounter = 0;
    void Start()
    {

    }
    void Update()
    {

    }

    protected void ChangeHealth(float value)
    {
        if (isShieldActive == false)
        {
            Currenthealth += value;
            if (Currenthealth <= 0)
            {
                dieCounter++;
                if (this.tag == "Player")
                {
                    GlobalEventManager.SendPlayer1Killed();
                }
                else if (this.tag == "Player2")
                {
                    GlobalEventManager.SendPlayer2Killed();
                }
                Death();
            }
            else if (Currenthealth >= 100)
            {
                Currenthealth = 100;
                UpdateHealthBar();
            }
            else
            {
                UpdateHealthBar();
            }
        }
        else
        {
            if (value >= 0)
            {
                Currenthealth += value;
                UpdateHealthBar();
                if (Currenthealth >= 100)
                {
                    Currenthealth = 100;
                    UpdateHealthBar();
                }
            }
        }
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if (other.tag == "Projectile")
        {
            enemyProjectile = other.GetComponent<Projectile>();
            ChangeHealth(-enemyProjectile.damage);
        }
    }
    private void Death()
    {
        this.gameObject.SetActive(false);
        Currenthealth = Health;
        UpdateHealthBar();
        this.transform.position = new Vector3(0, 3, 0);
        this.gameObject.SetActive(true);
    }

    void UpdateHealthBar()
    {
        HealthChanged?.Invoke((float)Currenthealth / Health);
    }

    protected void SetCurrentHP()
    {
        Currenthealth = Health;
    }
}
