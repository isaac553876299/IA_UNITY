using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class wander : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("redTank").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Wander();
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

        agent.destination = worldTarget;
    }

}
