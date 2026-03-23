using System.IO.Pipes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWander : MonoBehaviour
{
    [SerializeField] float wanderRadius = 20f;
    protected float maxChaseTime = 5f;
    protected float chaseTimer = 0f;
    [SerializeField] NavMeshAgent agent;
    protected void OnEnable()
    {
        
    }
    protected void OnDisable()
    {
        
    }
    protected bool hasArrived()
    {
        return agent.remainingDistance <= agent.stoppingDistance;
    }

    protected void FixedUpdate()
    {
        if (hasArrived())
        {
            agent.SetDestination(GetRandomPoint());
            //|| chaseTimer < Time.time Debug.Log(Time.time);
            //chaseTimer = Time.time + maxChaseTime; 
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(transform.position,wanderRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(agent.destination, 0.5f);
    }

    protected Vector3 GetRandomPoint() 
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        Vector3 randomPoint = transform.position + randomDirection;
        NavMeshHit hit;
        Vector3 finalPosition = transform.position;

        if(NavMesh.SamplePosition(randomPoint,out hit, 2f, 1)) 
        {
            finalPosition = hit.position; ;
        }
        return finalPosition;
    }

}
