using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TankPlayer : TankBaseClass
{
    [Header("Movement Stats")]
    [SerializeField] float _turnSpeed;
    Rigidbody _rb;

    float _movementValue;
    float _turnValue;
    public bool IsMoving = false;
    bool isSpeedActive = false;
    [Header("Shoot Stats")]
    [SerializeField] Rigidbody minePrefab;

    public enum ShootType { AutoFire, AntiTankMines };
    public ShootType shootType;

    [SerializeField] float autoFireDelay;
    public float FireTimer { get; set; }
    public float powerUpTime = 15;
    float _powerUpTimeReseter;
    public int damageMultiplier = 1;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        SetCurrentHP();
        _powerUpTimeReseter = powerUpTime;
    }
    void Start()
    {

    }
    void FixedUpdate()
    {
        DriveTank();
    }
    void Update()
    {
        SelectGun();
        EngineSound();
        MovePlayers();
        CheckIfIsMoving();
        ShieldActive();
        SpeedActive();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        string tag = other.tag;
        switch (tag)
        {
            case "PUShield":
                isShieldActive = true;

                break;
            case "PUSpeed":
                isSpeedActive = true;
                TankSpeed *= 2;

                break;
            case "PUDamage":
                if (damageMultiplier <= 4)
                {
                    damageMultiplier++;

                    thisProjectile.damage = 10 + (1.2f * damageMultiplier);
                }
                break;
            case "PUHealth":
                ChangeHealth(25);
                break;
        }
    }

    void DriveTank()
    {
        Vector3 movement = transform.forward * _movementValue * TankSpeed * Time.deltaTime;
        _rb.MovePosition(_rb.position + movement);
        float turn = _turnValue * _turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        _rb.MoveRotation(_rb.rotation * turnRotation);
    }

    void EngineSound()
    {
        if (Mathf.Abs(_movementValue) < 0.1f && Mathf.Abs(_turnValue) < 0.1)
        {
            if (MovementAudio.clip == TankMovementSound)
            {
                MovementAudio.clip = TankIdleSound;
                MovementAudio.Play();
            }
        }
        else
        {
            if (MovementAudio.clip == TankIdleSound)
            {
                MovementAudio.clip = TankMovementSound;
                MovementAudio.Play();
            }
        }
    }
    void SelectGun()
    {
        switch (shootType)
        {
            case ShootType.AutoFire:
                FireGun();
                break;
            case ShootType.AntiTankMines:
                AntiTankMinesLaying();
                break;
        }
    }


    void AntiTankMinesLaying()
    {
        if (gameObject.tag == "Player")
        {
            if (Input.GetKeyDown("space"))
            {
                Instantiate(minePrefab, transform.position - transform.forward * 2, transform.rotation);
            }
        }
        else if (gameObject.tag == "Player2")
        {
            if (Input.GetKeyDown("/"))
            {
                Instantiate(minePrefab, transform.position - transform.forward * 2, transform.rotation);
            }
        }
    }
    void FireGun()
    {

        FireTimer += Time.deltaTime;
        if (gameObject.tag == "Player")
        {
            if (FireTimer >= 0)
            {
                if (Input.GetKeyDown("space"))
                {
                    Rigidbody shot = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation).GetComponent<Rigidbody>();
                    shot.AddForce(bulletSpeed * transform.forward, ForceMode.VelocityChange);
                    FireTimer = 0;
                }
            }
        }
        else if (gameObject.tag == "Player2")
        {
            if (Input.GetKeyDown("/"))
            {
                Rigidbody shot = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation).GetComponent<Rigidbody>();
                shot.AddForce(bulletSpeed * transform.forward, ForceMode.VelocityChange);
                FireTimer = 0;
            }
        }
    }
    void MovePlayers()
    {
        if (gameObject.tag == "Player")
        {
            _movementValue = Input.GetAxis("Vertical");
            _turnValue = Input.GetAxis("Horizontal");
        }
        else if (gameObject.tag == "Player2")
        {
            _movementValue = Input.GetAxis("Vertical_2");
            _turnValue = Input.GetAxis("Horizontal_2");
        }
    }

    void CheckIfIsMoving()
    {
        if (Mathf.Abs(_movementValue) > 0.1f)
        {
            IsMoving = true;
        }
        else { IsMoving = false; }
    }

    void ShieldActive()
    {
        if (isShieldActive)
        {
            powerUpTime -= Time.deltaTime;
            if (powerUpTime <= 0)
            {
                isShieldActive = false;
                powerUpTime = _powerUpTimeReseter;
            }
        }
    }
    void SpeedActive()
    {
        if (isSpeedActive)
        {
            powerUpTime -= Time.deltaTime;
            print(powerUpTime);
            if (powerUpTime <= 0)
            {
                isSpeedActive = false;
                TankSpeed /= 2;
                powerUpTime = _powerUpTimeReseter;
            }
        }
    }

}
