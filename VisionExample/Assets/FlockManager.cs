using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject fish_prefab;
    public GameObject[] allFish;
    public int num_fish = 10;

    public bool bounded = true;
    public Vector3 swim_limits;

    public bool randomize = false;

    public bool follow_lider = false;
    public GameObject lider;

    public float radius;
    
    [Range(1.0f,5.0f)]
    public float min_speed = 1;
    [Range(5.0f, 10.0f)]
    public float max_speed = 2;
    [Range(10.0f, 40.0f)]
    public float neighbour_distance = 5;
    [Range(1.0f, 100.0f)]
    public float rotation_speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        allFish = new GameObject[num_fish];
        for (int i = 0; i < num_fish; ++i) {
            Vector3 random = new Vector3(Random.Range(-swim_limits.x, swim_limits.x), Random.Range(-swim_limits.y, swim_limits.y), Random.Range(-swim_limits.z, swim_limits.z));
            Vector3 pos = this.transform.position + random; // random position
            Vector3 randomize = new Vector3(Random.Range(1, 5), Random.Range(1,5), Random.Range(1,5)); // random vector direction
            allFish[i] = (GameObject)Instantiate(fish_prefab, pos, Quaternion.LookRotation(randomize));
            allFish[i].GetComponent<Flock>().myManager = this;
            allFish[i].GetComponent<Flock>().speed = Random.Range(min_speed,max_speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
