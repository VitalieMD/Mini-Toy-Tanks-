using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTank : TankBaseClass
{
    NavMeshAgent agent;
    public Transform[] patrolPoints;
    int index;
    Vector3 target;
    GameObject player;
    public float followDistance = 15f;
    //public float shootingDistance = 7;
    public float counter = 2;
    public float reseter = 2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        UpdateDestinationPoint();
    }

    void Update()
    {


    }
    void FixedUpdate()
    {
         if (Vector3.Distance(transform.position, player.transform.position) < followDistance)
        {
            agent.SetDestination(player.transform.position); //Follow player
        }
         else
        {
            Patroling();
        }
        Shooting(); 
    }

    void UpdateDestinationPoint()
    {
        target = patrolPoints[index].position;
        agent.SetDestination(target);
    }

    void IterateIndex()
    {
        index++;
        if (index == patrolPoints.Length)
        {
            index = 0;
        }
    }

    void Patroling()
    {
        UpdateDestinationPoint();
        if (Vector3.Distance(transform.position, target) < 3f)
        {
            IterateIndex();
            UpdateDestinationPoint();
        }
    }

    void Shooting()
    {
        counter += Time.deltaTime;

        if (followDistance - 2 > Vector3.Distance(transform.position, player.transform.position))
        {
            if (counter > reseter)
            {
                Rigidbody shot = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation).GetComponent<Rigidbody>();
                shot.AddForce(bulletSpeed * transform.forward, ForceMode.VelocityChange);
                counter = 0;
            }

        }
    }
}

