using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] float bullSpeed = 100.0f;
    Vector3 planet;
    Vector3 forward;

    void Start () {
        // Getting the planets position
        planet = GameObject.FindGameObjectWithTag("Planet").transform.position;
        // Store the forward vector when spawned
        SetForwardVec(transform.right);
        //transform.LookAt(transform.position, transform.forward);
    }

    private void FixedUpdate()
    {
        // If game is not over
        if (!Manager.instance.gameOver)
        {
            // Rotate around the center of the planet every second
            //transform.LookAt(transform.position, transform.right);
            transform.RotateAround(planet, forward, bullSpeed * Time.deltaTime);
            //Debug.Log("Its Working - Bullet Moving");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If it collides with the Duck
        if (collision.gameObject.transform.tag == "Duck")
            // Call HunterWin function in the Manager Script
            GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>().HunterWin();

        // If it collides with the Hunter
        if (collision.gameObject.transform.tag == "Hunter")
            // Call DuckWin function in the Manager Script
            GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>().DuckWin();

        if (collision.gameObject.transform.tag == "Poop")
            // Call DuckWin function in the Manager Script
            Debug.Log("Poop Collided");
            SetForwardVec(transform.forward += new Vector3(Random.Range(-5, 5), Random.Range(-0.9f, 0), Random.Range(-5, 5)));
        
    }

    void SetForwardVec(Vector3 v)
    {
        forward = v;
        transform.LookAt(transform.position, forward);
        //transform.forward += new Vector3(Random.Range(-5, 5), Random.Range(-0.9f, 0), Random.Range(-5, 5));
        //transform.LookAt(transform.position,forward);
    }
}
