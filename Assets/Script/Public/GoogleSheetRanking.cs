using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoogleSheetRanking : MonoBehaviour
{

    public List<UserInfo> userInfoList = new List<UserInfo>();

    [ContextMenu("테스트")]
    public void Test()
    {
    }

    public void SetScore(int score)
    {
        if (!GoogleSheetManager.instance.IsLogin())
        {
            return;
        }
        WWWForm form = new WWWForm();
        form.AddField("order", "setScore");
        form.AddField("score", score);
        StartCoroutine(GoogleSheetManager.instance.Post(form,()=> {
            //에러
            if (GoogleSheetManager.instance.RD.result == "ERROR")
            {
                switch (GoogleSheetManager.instance.RD.msg)
                {
                    case "다시 로그인 해주세요.":
                        break;
                }
            }
        }));
    }

    public void GetUserList(System.Action callback)
    {
        userInfoList.Clear();
        WWWForm form = new WWWForm();
        form.AddField("order", "getScoreList");
        StartCoroutine(GoogleSheetManager.instance.Post(form, () => {

            string value = GoogleSheetManager.instance.RD.value;
            string[] userList = value.Split('=');
            for (int i = 0; i < userList.Length - 1; i++)
            {
                string nickName = userList[i].Split('-')[0];
                string scoreString = userList[i].Split('-')[1];
                scoreString = scoreString.Substring(0, scoreString.Length - 2);
                scoreString.Trim();
                if (!string.IsNullOrEmpty(scoreString))
                {
                    int score = int.Parse(scoreString);
                    userInfoList.Add(new UserInfo(nickName, score));
                }
            }
            callback();
        }));
    }
}

public class UserInfo
{
    public string nickName;
    public int score;

    public UserInfo(string _nickName, int _score)
    {
        nickName = _nickName;
        score = _score;
    }
    public UserInfo()
    {
        nickName = "";
        score = 0;
    }
}
