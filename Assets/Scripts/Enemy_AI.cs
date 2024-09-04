using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask whatGound, whatPlayer;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPontSet;
    public float walkPointRange;

    //Attacking
    public float timeBWAttacks;
    public bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("FirstPersonController").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // In Sight or Attack Range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPontSet) SearchWalkPoint();
    }

    private void SearchWalkPoint()
    {
        
    }

    private void ChasePlayer()
    {

    }

    private void AttackPlayer()
    {

    }
}