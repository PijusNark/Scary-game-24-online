using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Enemy stats
    public float baseViewDistance = 15f; 
    public float patrolDistanceIncreasePerPage = 5f; 
    private float currentViewDistance;

    public float speedIncreasePerPage = 2f;
    private float currentSpeed;

    public Transform target;  
    private NavMeshAgent agent; 
    private Vector3 randomPoint; 

    private bool isPatrolling = true; 

    private bool hasTriggeredJumpscare = false; 

    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        currentViewDistance = baseViewDistance;
        currentSpeed = agent.speed;

        
        InvokeRepeating(nameof(RandomPoint), 0f, 3f);  
        randomPoint = transform.position;  
    }

    void Update()
    {
        
        agent.speed = currentSpeed;

        
        if (target != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);

            
            if (distanceToPlayer <= currentViewDistance && !hasTriggeredJumpscare)
            {
                agent.SetDestination(target.position);
                isPatrolling = false;  
                
                TriggerJumpscare();
            }
            else if (!isPatrolling)
            {
              
                isPatrolling = true;
                randomPoint = transform.position; 
                InvokeRepeating(nameof(RandomPoint), 0f, 3f); 
            }
        }
    }

    private void RandomPoint()
    {
        if (isPatrolling)
        {
           
            Vector3 randomDirection = Random.insideUnitSphere * 10f;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas);
            randomPoint = hit.position;
            agent.SetDestination(randomPoint);
        }
    }

    
    public void UpdateEnemyStats(int collectedPages)
    {
        currentViewDistance = baseViewDistance + (collectedPages * patrolDistanceIncreasePerPage);
        currentSpeed = agent.speed + (collectedPages * speedIncreasePerPage);

      
        Debug.Log("Updated Speed: " + currentSpeed);
        Debug.Log("Updated View Distance: " + currentViewDistance);
    }

    
    private void TriggerJumpscare()
    {
        if (!hasTriggeredJumpscare)
        {
            Debug.Log("Jumpscare! Enemy is too close to the player!");

            hasTriggeredJumpscare = true; 
        }
    }

    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, currentViewDistance);
    }
}
