using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    public int damage;

    public GameObject hatPrefab;
    public GameObject manapackPrefab;
    public GameObject scrollPrefab;
    public GameObject medpackPrefab;

    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Wizard").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }
    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    private void ChasePlayer()
    {
        transform.LookAt(player);
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        // Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            // Attack code here
            WizardMovement wizardMovement = player.GetComponent<WizardMovement>();
            if (wizardMovement.health != null)
            {
                wizardMovement.health -= damage ;// Adjust the damage value as needed
                Debug.Log("Attacking player...");
            }
            ///
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
        int rn = Random.Range(0, 101); //random number
        Vector3 pos = transform.position;
        pos.y = 1;
        if (rn == 1 || rn == 2)
        {
            Instantiate(hatPrefab, pos, Quaternion.identity);
        }
        else if (rn > 10 && rn < 15)
        {
            Instantiate(scrollPrefab, pos, Quaternion.identity);
        }
        else if (rn >= 15 && rn < 25)
        {
            Instantiate(medpackPrefab, pos, Quaternion.identity);
        }
        else if (rn >= 25 && rn < 35)
        {
            Instantiate(manapackPrefab, pos, Quaternion.identity);
        }
    }
}
