using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTest : MonoBehaviour
{

    public GameObject gameOver;
    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //pauses game if escape is pressed
        //change this to be a UI button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                resumeGame();
            }

            else
            {
                pauseGame();
            }
        }

    }

    public void pauseGame()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void resumeGame()
    {
        gameOver.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
