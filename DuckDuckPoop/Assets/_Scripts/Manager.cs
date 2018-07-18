using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Singleton Class of Manager
public class Manager : MonoBehaviour {

    public static Manager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    public bool gameOver;
    public Text hunterUI;
    public Text duckUI;
    public Button restart;

    string win = "You Win";
    string lose = "You Lose";

    new AudioSource audio;
    [SerializeField] AudioClip bg;
    [SerializeField] AudioClip huntWin;
    [SerializeField] AudioClip huntLose;

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
        DontDestroyOnLoad(gameObject);

        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    //Initializes the game for each level.
    void InitGame()
    {
        // Set game over to false
        gameOver = false;
    }

    private void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        //audio.Play();
        audio.volume = 0.4f;
    }

    public void HunterWin()
    {
        audio.PlayOneShot(huntWin, 1.0f);
        gameOver = true;
        duckUI.gameObject.SetActive(true);
        hunterUI.gameObject.SetActive(true);
        hunterUI.text = win;
        duckUI.text = lose;
        restart.gameObject.SetActive(true);
    }

    public void DuckWin()
    {
        audio.PlayOneShot(huntLose, 0.4f);
        gameOver = true;
        duckUI.gameObject.SetActive(true);
        hunterUI.gameObject.SetActive(true);
        hunterUI.text = lose;
        duckUI.text = win;
        restart.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Rematch();
        
    }

    public void Rematch()
    {
        SceneManager.LoadScene(1);
    }

}
