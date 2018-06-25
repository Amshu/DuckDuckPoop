using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    
    public void LoadFight()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitFight()
    {
        Application.Quit();
    }
}
