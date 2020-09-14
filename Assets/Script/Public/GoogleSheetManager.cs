using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    const string publicTableUrl = "https://docs.google.com/spreadsheets/d/1xm_y4Hnk_6qlPe1FaW8RW2v1ec_tjGGPxBX2GY-8ge0";
    const string tsv = "/export?format=tsv";
    const string range = "&range=A2:B"; // A2 부터 B까지 
    const string shitNumber = "&gid=0";

    const string authUrl = "";
    const string authScriptUrl = "https://script.google.com/macros/s/AKfycby3F_5Isp1UDeDc6nHN5_zCb9mmHPvEHkMeWvbWFwtCKfvhfhc/exec";

    private void Start()
    {
        StartCoroutine(Post());
    }

    IEnumerator Read()
    {
        string tempUrl = publicTableUrl + tsv + range + shitNumber;
        UnityWebRequest www = UnityWebRequest.Get(tempUrl);
        yield return www.SendWebRequest();
        string data = www.downloadHandler.text;
        print(data);
    }

    IEnumerator Get()
    {
        string tempUrl = authScriptUrl;
        UnityWebRequest www = UnityWebRequest.Get(tempUrl);
        yield return www.SendWebRequest();
        string data = www.downloadHandler.text;
        print(data);
    }

    IEnumerator Post()
    {
        string tempUrl = authScriptUrl;

        WWWForm form = new WWWForm();
        form.AddField("value", "값"); // 서버스크립트에서 p.value 의 value로 일치 시키는게 중요 

        UnityWebRequest www = UnityWebRequest.Post(tempUrl, form);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        print(data);
    }
}
