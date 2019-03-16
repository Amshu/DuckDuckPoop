using System.Collections;
using UnityEngine;

public class DuckController : MonoBehaviour {

    // Movement Variables
    [SerializeField] float moveSpeed = 15;
    [SerializeField] float dashForce = 500;
    private Vector3 moveDir;
    Rigidbody rb;

    // Ability Variables
    [SerializeField] GameObject poop;   // Poop Prefab'
    [SerializeField] float coolDownSec = 2.0f;
    [SerializeField] float duckCoolDownSec = 0.5f;
    
    bool canPoop = true;

    // Visual Feedback Variables
    GameObject ind;              // Cooldown Indicator
    Animator anim;
    new AudioSource audio;

    void Start()
    {
        // Get all the references to the variables
        rb = gameObject.GetComponent<Rigidbody>();

        ind = transform.GetChild(2).gameObject; // Reference to the indicator
        ind.GetComponent<MeshRenderer>().material.color = Color.green;
        anim = transform.GetChild(1).GetComponent<Animator>();
        audio = gameObject.GetComponent<AudioSource>();
        audio.volume = 0.4f;
    }

    public void MoveDuck(float x, float z, bool p)
    {
        moveDir = new Vector3(x, 0, z).normalized;

        //Animate
        anim.SetFloat("Move", Mathf.Abs(x) + Mathf.Abs(z));

        // When the poop button is pressed
        if (p && canPoop)
            DuckPoop();
    }

    public void DuckPoop()
    {
        // Store the current position
        Vector3 tempLoc = transform.position;

        // Do a dash
        rb.AddForce(transform.forward * dashForce * Time.deltaTime, ForceMode.Impulse);
        rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * dashForce * Time.deltaTime);

        // Poop
        GameObject.Instantiate(poop, tempLoc, transform.rotation);

        StartCoroutine("Reload");
    }

    // Cooldown function for ability
    IEnumerator Reload()
    {
        audio.Play();
        StartCoroutine("OnDuck"); // Start timer for ducking
        canPoop = false; // Disable ability
        ind.GetComponent<MeshRenderer>().material.color = Color.red;
       
        yield return new WaitForSeconds(coolDownSec);

        canPoop = true; // Enable ability
        ind.GetComponent<MeshRenderer>().material.color = Color.green;
    }

    // Cooldown for ducking
    IEnumerator OnDuck()
    {
        // Change the scale of the mesh
        transform.Find("Body").transform.localScale = new Vector3(1, 0.5f, 1);
        // Change the height of the collider
        GetComponent<CapsuleCollider>().height = 1;

        // If ducking is done, revert to normal
        yield return new WaitForSeconds(duckCoolDownSec);

        // Change the scale of the mesh
        transform.Find("Body").transform.localScale = new Vector3(1, 1, 1);
        // Change the height of the collider
        GetComponent<CapsuleCollider>().height = 2;
    }

    void FixedUpdate()
    {
        // If game is not over
        if (!Manager.instance.gameOver)
        {
            rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
        }
    }
}
