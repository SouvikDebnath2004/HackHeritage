using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy_AI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public GameObject projectile;

    public LayerMask whatIsGound, whatIsPlayer;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timer = 5;
    private float bulletTime;
    public Transform spawnPoint;
    public float enemySpeed;

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
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if(walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;   

        // Walkpoint reached
        if(distanceToWalkPoint.magnitude <1f)
            walkPointSet = false;

    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGound))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        if(!alreadyAttacked)
        {
            //Attack Code
            ShootAtPlayer();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBWAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;
        bulletTime = timer;
        GameObject bulletObj = Instantiate(projectile, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRigidbody = bulletObj.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(bulletRigidbody.transform.forward * enemySpeed);
        Destroy(bulletRigidbody, 5f);
    }
}