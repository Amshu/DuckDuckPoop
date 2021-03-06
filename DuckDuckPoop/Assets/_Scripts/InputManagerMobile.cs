﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManagerMobile : MonoBehaviour {

    public static InputManagerMobile instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    [SerializeField] FixedJoystick DuckJS;
    [SerializeField] FixedJoystick HunterJS;

    // Check for controllers
    int controllers = 0;

    // Duck Controls
    DuckController duck;
    float dx;
    float dz;


    // Hunter Controls
    HunterController hunter;
    float hx;
    float hz;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        //DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        // Get the duck controller script reference
        if (duck == null) duck = GameObject.FindGameObjectWithTag("Duck").GetComponent<DuckController>();
        // Get the duck controller script reference
        if (hunter == null) hunter = GameObject.FindGameObjectWithTag("Hunter").GetComponent<HunterController>();
	}

    private void OnLevelWasLoaded(int level)
    {
        // Get the duck controller script reference
        if (duck == null) duck = GameObject.FindGameObjectWithTag("Duck").GetComponent<DuckController>();
        // Get the duck controller script reference
        if (hunter == null) hunter = GameObject.FindGameObjectWithTag("Hunter").GetComponent<HunterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //CheckControllers();
        if(!Manager.instance.roundEnd) GetMainInput(controllers);
    }

    // Function which handles input according to controllers
    void GetMainInput(int c)
    {
        switch (c)
        {
            case 0:
                GetInput0();
                break;
            case 1:
                //GetInput1();
                break;
            case 2:
                //GetInput2();
                break;
            default:
                Debug.Log("Error with duck input");
                break;
        }
    }

    // Function which check for controllers
    void CheckControllers()
    {
        // Check for connected controllers
        if (Input.GetJoystickNames().Length > 0)
        {
            if (Input.GetJoystickNames().Length == 1) controllers = 1;

            else if (Input.GetJoystickNames().Length == 2) controllers = 2;
        }
        // If no controllers are connected
        else
        {
            controllers = 0;
        }
    }

    // Function which handles input for keyboard only
    void GetInput0()
    {
        dx = DuckJS.Horizontal;//Input.GetAxis("Horizontal");
        dz = DuckJS.Vertical;//Input.GetAxis("Vertical");
        duck.MoveDuck(-dz, dx, false);

        hx = HunterJS.Horizontal;
        hz = HunterJS.Vertical;
        hunter.MoveHunter(-hz, hx, false);
    }


    public void Shoot()
    {
        hunter.MoveHunter(hx, hz, true);
    }

    public void Poop()
    {
        duck.MoveDuck(dx, dz, true);
    }

    //// Function which handles input for 1 keyboard and 1 controller
    //void GetInput1()
    //{
    //    dx = Input.GetAxis("Horizontal");
    //    dz = Input.GetAxis("Vertical");
    //    duck.MoveDuck(dx, dz, Input.GetButtonDown("DDuck2"));

    //    hx = Input.GetAxis("Horizontal2");
    //    hz = Input.GetAxis("Vertical2");
    //    hunter.MoveHunter(hx, hz, Input.GetButtonDown("HFire2"));
    //}

    //// Function which handles ipnut for 2 controllers
    //void GetInput2()
    //{
    //    dx = Input.GetAxis("Horizontal1");
    //    dz = Input.GetAxis("Vertical1");
    //    duck.MoveDuck(dx, dz, Input.GetButtonDown("DDuck2"));

    //    hx = Input.GetAxis("Horizontal2");
    //    hz = Input.GetAxis("Vertical2");
    //    hunter.MoveHunter(hx, hz, Input.GetButtonDown("HFire2"));
    //}

}
