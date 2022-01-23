using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class RollerAgent : Agent
{
    public List<GameObject> disableTriggers = new List<GameObject>();
    public Transform child;
    private Vector3 newPos;
    [SerializeField] public float time1;
    [SerializeField] private float time2;

    public Transform[] possibleTargetSpawns;
    Rigidbody rBody;
    Vector3 targetStartPos;
    Vector3 playerStartPos;
    [SerializeField] private bool isWall = false;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        targetStartPos = target.transform.position;
        playerStartPos = transform.position;
        time2 = time1;
    }

    public Transform target;

    public override void OnEpisodeBegin()
    {
        time1 = time2;
        // If the Agent fell, zero its momentum
        if (this.transform.localPosition.y < 0)
        {
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(0, 0.5f, 0);
        }
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

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal * forceMultiplier);
        
        if (time1 >= 0)
        {
            time1 -= Time.deltaTime;
        }
        else
        {
            EndEpisode();
            time1 = time2;
        }
        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, target.localPosition);
        // Reached target
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        // Fell off platform
        else if (this.transform.localPosition.y < 0)
        {
            EndEpisode();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "wall")
        {
            isWall = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "wall")
        {
            isWall = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "positiveTr")
        {
            disableTriggers.Add(other.gameObject);
            other.GetComponent<BoxCollider>().enabled = false;
            other.GetComponent<MeshRenderer>().enabled = false;
            SetReward(1.0f);
        }
        if (other.tag == "negativeTr")
        {
            disableTriggers.Add(other.gameObject);
            other.GetComponent<BoxCollider>().enabled = false;
            other.GetComponent<MeshRenderer>().enabled = false;
            SetReward(-1.0f);
        }
        if (other.tag == "sp")
        {
            SetReward(-1.0f);
            EndEpisode();
        }
    }
}