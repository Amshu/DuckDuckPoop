using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public bool gameOver = false;
    public Text hunterUI;
    public Text duckUI;
    public Button restart;

    string win = "You Win";
    string lose = "You Lose";

    new AudioSource audio;
    [SerializeField] AudioClip bg;
    [SerializeField] AudioClip huntWin;
    [SerializeField] AudioClip huntLose;

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
