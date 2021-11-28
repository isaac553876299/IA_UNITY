using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mag : MonoBehaviour
{
    public int shells = 3;

    public void use()
    {
        --shells;
    }

    public void reload()
    {
        shells = 3;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
