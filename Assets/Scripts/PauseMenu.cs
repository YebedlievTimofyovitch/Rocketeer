using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuObject = null;
    [SerializeField] private Image CanvasColor = null;
    private bool isPaused = false;

    // Update is called once per frame
    private void Update()
    {
        
    }


    public void PauseGame()
    {
        if (!isPaused)
        {
            CanvasColor.color = new Color(0f, 0f, 0f, 0.25f);
            Time.timeScale = 0.0f;
            PauseMenuObject.SetActive(true);
            isPaused = true;
        }
        else
        {
            CanvasColor.color = new Color(0f, 0f, 0f, 0f);
            Time.timeScale = 1.0f;
            PauseMenuObject.SetActive(false);
            isPaused = false;
        }
    }

    public void ReturnToGame()
    {
        CanvasColor.color = new Color(0f, 0f, 0f, 0f);
        Time.timeScale = 1.0f;
        PauseMenuObject.SetActive(false);
        isPaused = false;
    }

    public void ReturnToMainMenu()
    {
        CanvasColor.color = new Color(0f, 0f, 0f, 0f);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
