using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public static UIManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    // Get all the UI elements
    // Round end UI
    public Text contUI;
    public Text countDown;
    // Game end UI
    public Button restart;
    public Button mainMenu;
    public Text hunterUI; 
    public Text duckUI;

    string win = "You Win";
    string lose = "You Lose";

    public GameObject scoreBoard;
    GameObject[] icons = new GameObject[5];

    public GameObject huntIcon;
    public GameObject duckIcon;

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

        ClearUI();
    }

    private void Start()
    {
        int i = 0;
        while(i < icons.Length)
        {
            if (icons[i] == null)
            {
                icons[i] = scoreBoard.transform.GetChild(i).gameObject;
            }
            i++;
        }
        ClearBoard();
    }

    public void ClearUI()
    {
        contUI.gameObject.SetActive(false);
        countDown.gameObject.SetActive(false);
    
        duckUI.gameObject.SetActive(false);
        hunterUI.gameObject.SetActive(false);
        restart.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
    }

    public void ClearBoard()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            if (icons[i].transform.childCount != 0)
                Destroy(icons[i].transform.GetChild(0).gameObject);
        }
    }

    public void DispHunterWin()
    {
        duckUI.gameObject.SetActive(true);
        hunterUI.gameObject.SetActive(true);
        hunterUI.text = win;
        duckUI.text = lose;
        DispGameOver();
    }

    public void DispDuckWin()
    {
        duckUI.gameObject.SetActive(true);
        hunterUI.gameObject.SetActive(true);
        hunterUI.text = lose;
        duckUI.text = win;
        DispGameOver();
    }

    public void DispHunterRound(int index)
    {
        Instantiate(huntIcon, icons[index -1].transform);
    }

    public void DispDuckRound(int index)
    {
        Instantiate(duckIcon, icons[index - 1].transform);
    }

    void DispGameOver()
    {
        restart.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(true);
    }

    public void DispCountdown(int count)
    {
        contUI.gameObject.SetActive(true);
        countDown.gameObject.SetActive(true);
        countDown.text = count.ToString();
    }
}
