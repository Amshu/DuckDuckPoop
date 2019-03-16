using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// Singleton Class of Manager
public class Manager : MonoBehaviour {

    public static Manager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    int hunterWin;
    int duckWin;
    public bool roundEnd;
    public bool gameOver;

    //public Text hunterUI;
    //public Text duckUI;
    //public Text contUI;
    //public Button restart;

    //string win = "You Win";
    //string lose = "You Lose";

    bool volDisabled;
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

    private void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            Destroy(this);
        }
    }

    //Initializes the game for each level.
    void InitGame()
    {
        // Set game over to false
        gameOver = false;

        roundEnd = false;

        if(audio == null)
            audio = gameObject.GetComponent<AudioSource>();
        
        if(!volDisabled)
            audio.Play();
    }

    public void HunterWin()
    {
        audio.PlayOneShot(huntWin, 1.0f);
        hunterWin += 1;
        // Display the winners icon on the scoreboard
        UIManager.instance.DispHunterRound(duckWin + hunterWin);
        //Debug.Log(hunterWin);
        RoundEnd();
    }

    public void DuckWin()
    {
        audio.PlayOneShot(huntLose, 0.4f);
        duckWin += 1;
        // Display the winners icon on the scoreboard
        UIManager.instance.DispDuckRound(duckWin + hunterWin);
        //Debug.Log(duckWin);
        RoundEnd();
    }

    public void RoundEnd()
    {
        roundEnd = true;
        if (duckWin >= 3)
        {
            gameOver = true;
            UIManager.instance.DispDuckWin();
        }
        else if (hunterWin >= 3)
        {
            gameOver = true;
            UIManager.instance.DispHunterWin();
        }
        else
        {
            StartCoroutine(StartCountdown(3.0f));
        }
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.P))
            Rematch();

        if (Input.GetKeyDown(KeyCode.O))
            MainMenu();

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (volDisabled)
            {
                volDisabled = false;
                audio.Play();
            }
            else
            {
                volDisabled = true;
                audio.Stop();
            }
       }
       */ 
    }

    public void ResetRound()
    {
        // Countdown to next round
        roundEnd = false;
        SceneManager.LoadScene(1);
        InitGame();
    }

    public void Rematch()
    {
        hunterWin = 0;
        duckWin = 0;

        ResetRound();
        UIManager.instance.ClearBoard();
        UIManager.instance.ClearUI();
    }

    public void MainMenu()
    {
        UIManager.instance.ClearBoard();
        UIManager.instance.ClearUI();
        SceneManager.LoadScene(0);
        Destroy(UIManager.instance.gameObject);
        Destroy(gameObject);
    }

    private IEnumerator StartCountdown(float countdownValue)
    {
        float currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            UIManager.instance.DispCountdown((int)currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        UIManager.instance.ClearUI();
        ResetRound();
    }

    public void ToggleMusic()
    {
        if (volDisabled)
        {
            volDisabled = false;
            audio.Play();
        }
        else
        {
            volDisabled = true;
            audio.Stop();
        }
    }
}
