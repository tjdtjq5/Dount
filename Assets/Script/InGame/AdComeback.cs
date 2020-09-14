using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdComeback : MonoBehaviour
{
    public GameObject gameOverPannel;
    public GameObject adComebackPannel;
    public GameObject reStartPannel;
    public Restart restart;

    int initialTime = 10;
    public Text countdownText;
    IEnumerator countdownCoroutine;

    public void AdComebackOpen()
    {
        countdownCoroutine = CountdownCoroutine();
        StartCoroutine(countdownCoroutine);
    }
    public void AdComebackClose()
    {
        StopCoroutine(countdownCoroutine);

        reStartPannel.SetActive(true);
        restart.RestartOpen();
        adComebackPannel.SetActive(false);
    }

    IEnumerator CountdownCoroutine()
    {
        int waitTime = initialTime;
        for (int i = 0; i < initialTime; i++)
        {
            countdownText.text = waitTime + "";
            yield return new WaitForSeconds(1);
            waitTime--;
        }
        AdComebackClose();
    }

    public void AdReword()
    {
        gameOverPannel.SetActive(false);
        adComebackPannel.SetActive(false);
        reStartPannel.SetActive(false);

        InGame.instance.ComebackStart();
    }
}
