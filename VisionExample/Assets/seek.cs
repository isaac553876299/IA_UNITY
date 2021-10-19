using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seek : MonoBehaviour
{
    public GameObject target;

    float freq = 0.0f;
    public float max_freq = 0.0f;

    public float movSpeed = 5.0f;
    public float maxSpeed = 10.0f;
    public float acceleration = 0.5f;

    public float turnSpeed = 5.0f;
    public float maxTurnSpeed = 10.0f;
    public float turnAcceleration = 0.5f;

    public float stopDistance = 2.0f;

    Vector3 movement;
    Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void do_seek()
    {
        freq += Time.deltaTime;
        if (freq > max_freq)
        {
            freq = 0.0f;
            {
                if (Vector3.Distance(target.transform.position, transform.position) < stopDistance)
                    return;
                Seek();   // calls to this function should be reduced
                turnSpeed += turnAcceleration * Time.deltaTime;
                turnSpeed = Mathf.Min(turnSpeed, maxTurnSpeed);
                movSpeed += acceleration * Time.deltaTime;
                movSpeed = Mathf.Min(movSpeed, maxSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
                transform.position += transform.forward.normalized * movSpeed * Time.deltaTime;
            }
        }
    }

    void Seek()
    {
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0f;
        movement = direction.normalized * acceleration;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(movement.x, movement.z);
        rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
