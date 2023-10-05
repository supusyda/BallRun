using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject pauseMenu;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Play()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void ToMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
        Time.timeScale = 1;

    }
    public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;


    }
}
