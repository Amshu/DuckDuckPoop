using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterController : MonoBehaviour {

    // Movement Variables
    [SerializeField] float moveSpeed = 15;
    private Vector3 moveDir;
    Rigidbody rb;

    // Ability Variables
    [SerializeField] GameObject bullet;
    GameObject spawnLoc;
    GameObject ind;
    [SerializeField] float coolDownSec = 2.0f;
    bool canShoot = true;

    // Visual Feedback Variables
    Animator anim;
    new AudioSource audio;

    Manager manager;

    private void Start()
    {
        // Get all the references to the variables
        rb = gameObject.GetComponent<Rigidbody>();
        spawnLoc = transform.GetChild(2).gameObject;
        
        ind = transform.GetChild(3).gameObject; // Reference to the indicator
        ind.GetComponent<MeshRenderer>().material.color = Color.green;
        anim = transform.GetChild(1).GetComponent<Animator>();
        audio = gameObject.GetComponent<AudioSource>();
        audio.volume = 0.4f;

        // Reference to the Manager Script
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
    }

    public void MoveHunter(float x, float z, bool s)
    {
        moveDir = new Vector3(x, 0, z).normalized;

        //Animate
        anim.SetFloat("Move", Mathf.Abs(x) + Mathf.Abs(z));
        
        //Debug.Log(canShoot);    
        // If Fire button is pressed
        if (s && canShoot)
            HunterShoot();
    }

    // Shoot function
    void HunterShoot()
    {
        //Debug.Log("Fire");
        audio.Play();
        GameObject.Instantiate(bullet, spawnLoc.transform.position, spawnLoc.transform.rotation);

        // Start cooldown
        canShoot = false;
        ind.GetComponent<MeshRenderer>().material.color = Color.red;
        StartCoroutine("Reload");
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(coolDownSec);
        canShoot = true;
        ind.GetComponent<MeshRenderer>().material.color = Color.green;
    }

    private void FixedUpdate()
    {
        // If game is not over
        if (!manager.gameOver)
        {
            rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
        }
    }
}
