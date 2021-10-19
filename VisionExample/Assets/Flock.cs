using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager;
    // Start is called before the first frame update
    public float speed;
    public Vector3 direction;
    int loop = 0;
    public bool turning = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (loop++ > 10)
        {
            loop = 0;
        }
        FollowRules();
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotation_speed * Time.deltaTime);
        transform.Translate(-(Time.deltaTime * speed), 0.0f, 0.0f);
        Debug.Log((myManager.transform.position - transform.position).magnitude);

    }

    void FollowRules()
    {
        Vector3 cohesion = Vector3.zero;
        Vector3 align = Vector3.zero;
        Vector3 separation = Vector3.zero;
        int num = 0;
        foreach (GameObject go in myManager.allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= myManager.neighbour_distance)
                {
                    num++;

                    cohesion += go.transform.position;
                    
                    align += go.GetComponent<Flock>().direction;
                    

                    separation -= (transform.position - go.transform.position) / (distance * distance);
                }
            }
        }
        if (num > 0)
        {
            cohesion = (cohesion / num - transform.position).normalized * speed;

            align /= num;
            speed = Mathf.Clamp(align.magnitude, myManager.min_speed, myManager.max_speed);
        }

        direction = (cohesion + align + separation).normalized * speed;
    }
    
}
