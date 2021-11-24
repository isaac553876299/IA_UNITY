using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class patrol : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    public GameObject[] waypoints;
    int patrolWP = -1;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("blueTank").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            patrolWP = (patrolWP + 1) % waypoints.Length;
            agent.destination=(waypoints[patrolWP].transform.position);
        }
    }

}
