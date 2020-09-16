using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{

    public GameObject gameOverPannel;
    public GameObject adComebackPannel;
    public GameObject reStartPannel;

    public Text playTimeText;

    public void RestartOpen()
    {
        int time = (int)Time.instance.GetTime();
        int minute = time / 60;
        int second = time % 60;
        string playTime = minute + ":" + second;
        playTimeText.text = "플레이 타임 : " + playTime; 
    }

    public void ReGame()
    {
        gameOverPannel.SetActive(false);
        adComebackPannel.SetActive(false);
        reStartPannel.SetActive(false);

        InGame.instance.Restart();
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
