using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float sightRange = 20f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private NavMeshAgent agent;

    private enum State { Patrol, Chase, Attack }
    private State state;
    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
        state = State.Patrol;
        agent= GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < sightRange)
        {
            state = State.Chase;
        }

        if (distanceToPlayer > sightRange)
        {
            state = State.Patrol;
        }

        if (distanceToPlayer < attackRange)
        {
            state = State.Attack;
            Attack();
        }

        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
        }
    }

    private void Patrol()
    {
        agent.destination = startingPosition;
    }

    private void Chase()
    {
        agent.destination = playerTransform.position;
    }

    private void Attack()
    {
        // Cause damage to player
        Player playerHealth = playerTransform.GetComponent<Player>();
        playerHealth.TakeDamage(attackDamage);
    }
}
