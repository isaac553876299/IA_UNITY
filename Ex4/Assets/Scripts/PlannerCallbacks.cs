using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generated.Semantic.Traits;

public class PlannerCallbacks : MonoBehaviour
{
    public GameObject cop;
    public GameObject treasure;

    public Moves moves;
    public UnityEngine.AI.NavMeshAgent agent;
    public Robber trt;

    public void Steal()
    {
        Debug.Log("Steal");
        treasure.GetComponent<Renderer>().enabled = false;
    }

    public IEnumerator Seek()
    {
        Debug.Log("Approach");
        agent.SetDestination(treasure.transform.position);
        while ((Vector3.Distance(treasure.transform.position, transform.position) > 2f) &&
               (Vector3.Distance(treasure.transform.position, cop.transform.position) > 10f))
            yield return null;
        if (Vector3.Distance(treasure.transform.position, cop.transform.position) < 10f)
        {
            trt.CopAway = false;
        }
        else
        {
            trt.Ready2Steal = true;
        }
    }

    public IEnumerator Wander()
    {
        Debug.Log("Wander");
        while (Vector3.Distance(treasure.transform.position, cop.transform.position) < 10f)
        {
            moves.Wander();
            yield return null;
        }
    }    
}
