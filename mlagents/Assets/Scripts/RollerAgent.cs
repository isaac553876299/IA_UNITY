using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class RollerAgent : Agent
{
    [SerializeField] public float time1;
    [SerializeField] private float time2;
    public List<GameObject> disableTriggers = new List<GameObject>();

    public Transform[] possibleTargetSpawns;
    Rigidbody rBody;
    [SerializeField] private bool isWall = false;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        time2 = time1;
    }

    public Transform target;

    public override void OnEpisodeBegin()
    {
        time1 = time2;
        for (int i = 0; i < disableTriggers.Count; i++)
        {
            disableTriggers[i].GetComponent<MeshRenderer>().enabled = true;
            disableTriggers[i].GetComponent<BoxCollider>().enabled = true;
            disableTriggers[i].GetComponent<BoxCollider>().isTrigger = true;
            disableTriggers.RemoveAt(i);
        }
	int random = Random.Range(0,possibleTargetSpawns.Length);
        transform.position = possibleTargetSpawns[random].position;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
        sensor.AddObservation(isWall);
        isWall = false;
    }

    public float forceMultiplier = 10;

    public override void OnActionReceived(ActionBuffers actions)
    {
		Vector3 control =
			new Vector3(actions.ContinuousActions[0], 0, actions.ContinuousActions[1]);
        rBody.AddForce(control * forceMultiplier);
        
        if (time1 >= 0) time1 -= Time.deltaTime;
        else
        {
            EndEpisode();
            time1 = time2;
        }

        if (Vector3.Distance(this.transform.localPosition, target.localPosition) < 1.5f)
        {
            SetReward(1.0f);
            EndEpisode();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
		isWall = (collision.transform.tag == "wall");
	}
	private void OnCollisionStay(Collision collision)
    {
		isWall = (collision.transform.tag == "wall");
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "positiveTr")
        {
            SetReward(0.3f);
            disableTriggers.Add(other.gameObject);
            other.GetComponent<BoxCollider>().enabled = false;
            other.GetComponent<MeshRenderer>().enabled = false;
        }
        if (other.tag == "negativeTr")
        {
            SetReward(-0.5f);
            disableTriggers.Add(other.gameObject);
            other.GetComponent<BoxCollider>().enabled = false;
            other.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}