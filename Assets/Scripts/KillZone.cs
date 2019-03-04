using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{

    private TimeSpan TimeElapsed;
    public GameObject GameTimer = null;
    public GameObject GameOverScreen = null;
    public Text GameOverText = null;


    void Awake()
    {
        if (GameOverScreen != null)
        {
            GameOverScreen.SetActive(false);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {

        //if not ploayer then exit
        if (!other.CompareTag("Player"))
        {
            return;
        }
        else
        {
            if (GameTimer != null)
            {
                GameTimer.GetComponent<GameTimerScript>().Stop();

            }
            Time.timeScale = 0;
            if (GameOverScreen != null)
            {
                GameOverScreen.SetActive(true);
            }
            TimeElapsed = GameTimer.GetComponent<GameTimerScript>().TimeElapsed;
            if (GameOverText != null)
            {
                GameOverText.text = "Your lasted for: \n" + TimeElapsed.Minutes + " minutes, " + ((double)TimeElapsed.Seconds + ((double)TimeElapsed.Milliseconds)/1000) + " seconds. \nTry again for a longer time";
            }
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level0");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
