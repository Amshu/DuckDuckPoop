using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour {

    [SerializeField] float AttractMultiplier = 1.0f;

    [SerializeField] FauxGravityAttractor attractor;
    private Transform myTransform;

   
    public float GetMultiplier()
    {
        return AttractMultiplier;
    }
       
    void Start()
    {
        // Get Attractor component from Planet
        if (attractor == null) attractor = GameObject.FindWithTag("Planet").GetComponent<FauxGravityAttractor>();

        // Get rigid body component reference
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Disable Rotation
        rb.useGravity = false;                                // Disable Gravity
        
        // Get transform reference
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        attractor.Attract(myTransform);
    }
}
