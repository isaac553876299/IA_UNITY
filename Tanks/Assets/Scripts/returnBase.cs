using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnBase : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = (target.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
