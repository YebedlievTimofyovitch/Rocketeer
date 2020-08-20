using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text HighScoreMenuText = null;

    private void Start()
    {
        HighSoreMainMenu();
    }

    private void HighSoreMainMenu()
    {
        HighScoreMenuText.text = @"High Score
_________
" + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void LoadGameLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void EraseHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);

        HighScoreMenuText.text = @"High Score
_________
0";
    }
}
