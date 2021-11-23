using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class wander : MonoBehaviour
{
    public bool wandr = false;
    public bool patrol = true;

    public NavMeshAgent agent;
    public GameObject target;

    public GameObject[] waypoints;
    int patrolWP = -1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (patrol && !wandr) Patrol();
        if (wandr && !patrol) Wander();
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            patrolWP = (patrolWP + 1) % waypoints.Length;
            Seek(waypoints[patrolWP].transform.position);
        }
    }

    void Wander()
    {
        float radius = 10f;
        float offset = 3f;

        Vector3 localTarget = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        localTarget.Normalize();
        localTarget *= radius;
        localTarget += new Vector3(0, 0, offset);

        Vector3 worldTarget = agent.transform.TransformPoint(localTarget);
        worldTarget.y = 0f;

        Seek(worldTarget);
    }

    void Seek(Vector3 worldTarget)
    {
        agent.destination = worldTarget;
    }
}
