using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    bool duckHit;
    bool huntHit;

    [SerializeField] float bullSpeed = 100.0f;
    Vector3 planet;
    Vector3 forward;

    void Start () {
        duckHit = false;
        huntHit = false;
        // Getting the planets position
        planet = GameObject.FindGameObjectWithTag("Planet").transform.position;
        // Store the forward vector when spawned
        SetForwardVec(transform.right);
        //transform.LookAt(transform.position, transform.forward);
    }

    private void FixedUpdate()
    {
        // If game is not over
        //if (!Manager.instance.gameOver && !Manager.instance.roundEnd)
        //{
            // Rotate around the center of the planet every second
            transform.RotateAround(planet, forward, bullSpeed * Time.deltaTime);
            //Debug.Log("Its Working - Bullet Moving");
       // }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!Manager.instance.gameOver && !Manager.instance.roundEnd)
        { 
            // If it collides with the Duck
            if (collision.gameObject.transform.tag == "Duck")
                if (!duckHit)
                {
                    duckHit = true;
                    Manager.instance.HunterWin();
                }
                else return;

            // If it collides with the Hunter
            if (collision.gameObject.transform.tag == "Hunter")
                if (!huntHit)
                {
                    huntHit = true;
                    Manager.instance.DuckWin();
                }
                else return;

            if (collision.gameObject.transform.tag == "Poop")
                SetForwardVec(transform.forward += new Vector3(Random.Range(-5, 5), Random.Range(-0.9f, 0), Random.Range(-5, 5)));
        }
    }

    void SetForwardVec(Vector3 v)
    {
        forward = v;
        transform.LookAt(transform.position, forward);
        //transform.forward += new Vector3(Random.Range(-5, 5), Random.Range(-0.9f, 0), Random.Range(-5, 5));
        //transform.LookAt(transform.position,forward);
    }
}
