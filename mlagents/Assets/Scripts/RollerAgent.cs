using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class RollerAgent : Agent
{
    Rigidbody rigidBody;
    public Transform target;
    public float forceMultiplier = 10;

    public Transform child;
    public float wallMinDistance;
    [SerializeField] private float fp;
    [SerializeField] private float fm;
    [SerializeField] private float rp;
    [SerializeField] private float rm;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        this.transform.localPosition = new Vector3(10, 0.5f,-10);
        this.rigidBody.velocity = Vector3.zero;
        this.rigidBody.angularVelocity = Vector3.zero;

        target.transform.localPosition = new Vector3(
            Random.value * 2 + 10,
            0.5f,
            Random.value * 2 - 10);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        sensor.AddObservation(rigidBody.velocity.x);
        sensor.AddObservation(rigidBody.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        rigidBody.AddForce(controlSignal * forceMultiplier);

        if (this.transform.localPosition.y < 0)
        {
            // fell off
            EndEpisode();
        }

        RaycastHit hitForward;
        RaycastHit hitRight;
        RaycastHit hitMinusForward;
        RaycastHit hitMinusRight;
        // RAYCAST FORWARD +
        if (Physics.Raycast(child.transform.position, child.transform.forward, out hitForward, wallMinDistance))
        {
            if (hitForward.transform.tag == "wall")
            {
                fp = hitForward.distance;
            }
            Debug.DrawLine(child.transform.position, hitForward.transform.position, Color.black);
        }
        // RAYCAST RIGHT +
        if (Physics.Raycast(child.transform.position, child.transform.right, out hitRight, wallMinDistance))
        {
            if (hitRight.transform.tag == "wall")
            {
                rp = hitRight.distance;
            }
            Debug.DrawLine(child.transform.position, hitRight.transform.position, Color.black);
        }
        // RAYCAST FORWARD -
        if (Physics.Raycast(child.transform.position, -child.transform.forward, out hitMinusForward, wallMinDistance))
        {
            if (hitMinusForward.transform.tag == "wall")
            {
                fm = hitMinusForward.distance;
            }
            Debug.DrawLine(child.transform.position, hitMinusForward.transform.position, Color.black);
        }
        // RAYCAST RIGHT -
        if (Physics.Raycast(child.transform.position, -child.transform.right, out hitMinusRight, wallMinDistance))
        {
            if (hitMinusRight.transform.tag == "wall")
            {
                rm = hitMinusRight.distance;
            }
            Debug.DrawLine(child.transform.position, hitMinusRight.transform.position, Color.black);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "target")
        {
            SetReward(1.0f);
            EndEpisode();
        }
        if (collision.gameObject.tag == "wall")
        {
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }

    void Update()
    {
        
    }
}
