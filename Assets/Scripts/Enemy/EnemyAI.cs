using StarterAssets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    
    [SerializeField] float chaseRange;
    [SerializeField] float turnSpeed;

    CapsuleCollider capsuleCollider;
    EnemyHealth enemyHealth;
    Transform target;
    NavMeshAgent navMeshAgent;
    bool isProvoked = false;
    float distanceToTarget = Mathf.Infinity;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyHealth = GetComponent<EnemyHealth>();
        target = FindFirstObjectByType<FirstPersonController>().transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (enemyHealth.IsDead)
        {
            enabled = false;
            navMeshAgent.enabled = false;
            capsuleCollider.enabled = false;
        }

        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
        else if (distanceToTarget > chaseRange)
        {
            isProvoked = false;
            GetComponent<Animator>().SetTrigger("Idle");
        }
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    void EngageTarget()
    {
        FaceTarget();

        if (distanceToTarget > navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("Move");
        navMeshAgent.SetDestination(target.transform.position);
    }

    void AttackTarget()
    {
        GetComponent<Animator>().SetBool("Attack", true);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
