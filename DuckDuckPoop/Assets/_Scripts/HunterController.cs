using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterController : MonoBehaviour {

    [SerializeField] float moveSpeed = 15;
    private Vector3 moveDir;
    Rigidbody rb;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnLoc;
    [SerializeField] GameObject ind;
    bool canShoot = true;

    [SerializeField] Animator anim;
    new AudioSource audio;

    Manager manager;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        audio = gameObject.GetComponent<AudioSource>();
        // Reference to the Manager Script
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        ind.GetComponent<Material>().color = Color.green;
    }

    private void Update()
    {
        // Get controls and apply
        float x = Input.GetAxis("Horizontal2");
        float z = Input.GetAxis("Vertical2");
        moveDir = new Vector3(x, 0, z).normalized;

        //Animate
        anim.SetFloat("Move", Mathf.Abs(x) + Mathf.Abs(z));

        // If Fire button is pressed
        if (Input.GetButtonDown("HFire"))// && canShoot)
        {
            //Debug.Log("Fire");
            audio.Play();
            GameObject.Instantiate(bullet, spawnLoc.transform.position, spawnLoc.transform.rotation);

            // Start cooldown
            canShoot = false;
            ind.GetComponent<Material>().color = Color.red;
            //StartCoroutine("Reload");
        }
    }

    private void FixedUpdate()
    {
        // If game is not over
        if (!manager.gameOver)
        {
            rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(2.0f);
        canShoot = true;
        ind.GetComponent<Material>().color = Color.green;
    }
}
