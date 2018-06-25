using System.Collections;
using UnityEngine;

public class DuckController : MonoBehaviour {

    [SerializeField] float moveSpeed = 15;
    [SerializeField] float dashForce = 30;
    private Vector3 moveDir;
    Rigidbody rb;

    bool duck = false;
    [SerializeField] GameObject poop;
    [SerializeField] GameObject ind;
    bool canPoop = true;


    [SerializeField] Animator anim;
    new AudioSource audio;

    // Reference to the Manager Script
    Manager manager;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        audio = gameObject.GetComponent<AudioSource>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        ind.GetComponent<Material>().color = Color.green;

    }

    void Update()
    {
        // Get controls and apply
        float x = Input.GetAxis("Horizontal1");
        float z = Input.GetAxis("Vertical1");
        moveDir = new Vector3(x, 0, z).normalized;

        //Animate
        anim.SetFloat("Move", Mathf.Abs(x) + Mathf.Abs(z));

        // If duck is pressed
        if (Input.GetButtonDown("DDuck") )//&& canPoop)
        {
            // Making it into a ducking state
            duck = true;

            // Store the current position
            Vector3 tempLoc = transform.position;

            // Change the scale of the mesh
            transform.Find("Body").transform.localScale = new Vector3(1, 0.5f, 1);
            // Change the height of the collider
            GetComponent<CapsuleCollider>().height = 1;
            // Do a dash
            //rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
            rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * dashForce * Time.deltaTime);

            // Poop
            GameObject.Instantiate(poop, tempLoc, transform.rotation);

            // Start cooldown
            canPoop = false;
            ind.GetComponent<Material>().color = Color.red;
            //StartCoroutine("Reload");
        }

        if (Input.GetButtonUp("DDuck"))
        {
            // If ducking is done, revert to normal
            if (duck)
            {
                audio.Play();
                // Change the scale of the mesh
                transform.Find("Body").transform.localScale = new Vector3(1, 1, 1);
                // Change the height of the collider
                GetComponent<CapsuleCollider>().height = 2;
            }
            duck = false;
        }
    }

    void FixedUpdate()
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
        canPoop = true;
        ind.GetComponent<Material>().color = Color.green;
    }
}
