using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time : MonoBehaviour
{
    public static Time instance;

    public Text text;

    float sec;
    float waitTime = 0.02f;
    WaitForSeconds wait;
    IEnumerator timeCoroutine;

    private void Awake()
    {
        instance = this;
    }

    public void TimeStart()
    {
        sec = 0;
        wait = new WaitForSeconds(waitTime);
        timeCoroutine = TimeCoroutine();
        StartCoroutine(timeCoroutine);
    }
    public void TimePause()
    {
        StopCoroutine(timeCoroutine);
    }
    public void TimeReStart()
    {
        StartCoroutine(timeCoroutine);
    }
    public float GetTime()
    {
        return sec;
    }
    IEnumerator TimeCoroutine()
    {
        while (true)
        {
            yield return wait;
            sec += waitTime;

            int time = (int)sec;
            int minute = time / 60;
            int second = time % 60;
            text.text = minute + ":" + second;
        }
    }
}
