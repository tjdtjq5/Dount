using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
    public static GoogleSheetManager instance;

    const string publicTableUrl = "https://docs.google.com/spreadsheets/d/1xm_y4Hnk_6qlPe1FaW8RW2v1ec_tjGGPxBX2GY-8ge0";
    const string tsv = "/export?format=tsv";
    const string range = "&range=A2:B"; // A2 부터 B까지 
    const string shitNumber = "&gid=0";

    const string authUrl = "";
    const string authScriptUrl = "https://script.google.com/macros/s/AKfycby3F_5Isp1UDeDc6nHN5_zCb9mmHPvEHkMeWvbWFwtCKfvhfhc/exec";
    [SerializeField] public string id = "최성섭";[SerializeField] public string password = "비밀번호";


    public ResponseData RD = new ResponseData();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Login();
    }

    [ContextMenu("테스트")]
    public void Test()
    {
        GetValue();
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

    public IEnumerator Post(WWWForm form, System.Action callback = null)
    {
        string tempUrl = authScriptUrl;

        using (UnityWebRequest www = UnityWebRequest.Post(tempUrl,form))
        {
            yield return www.SendWebRequest();

            if (www.isDone) Response(www.downloadHandler.text);
            else print("웹의 응답이 없습니다.");
        }

        if (callback != null) callback();
    }

    public void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        RD = JsonUtility.FromJson<ResponseData>(json);

        if (RD.result == "ERROR")
        {
            print(RD.order + "을 실행 할 수 없습니다. 에러 메세지 : " + RD.msg);
            return;
        }

        print(RD.order + "을 실행 했습니다. 메세지 : " + RD.msg);

        if (RD.order == "getValue")
        {
            Debug.Log(RD.value);
        }
    } 

    // 회원가입
    public void Register()
    {
        id.Trim(); password.Trim();
        if (id == "" || password == "")
        {
            Debug.Log("값을 입력해주세요");
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "register");
        form.AddField("id", id);
        form.AddField("password", password);

        StartCoroutine(Post(form));
    }

    //로그인 
    public void Login()
    {
        id.Trim(); password.Trim();
        if (id == "" || password == "")
        {
            Debug.Log("값을 입력해주세요");
            return;
        }

        WWWForm form = new WWWForm();
        form.AddField("order", "login");
        form.AddField("id", id);
        form.AddField("password", password);

        StartCoroutine(Post(form));
    }

    public bool IsLogin()
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password))
        {
            return false;
        }
        return true;
    }

    public void Logout()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "logout");

        StartCoroutine(Post(form));
    }

    public void SetValue(string value)
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "setValue");
        form.AddField("value", value);

        StartCoroutine(Post(form));
    }
    public void GetValue()
    {
        WWWForm form = new WWWForm();
        form.AddField("order", "getValue");

        StartCoroutine(Post(form));
    }
}

public class ResponseData
{
    public string order, result, msg, value;
}