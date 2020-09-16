using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    List<UserInfo> tempUserInfoList = new List<UserInfo>();

    public GameObject rankingObj;
    public GameObject userInfoPrepab;
    public Transform context;

    public void RankingOpen()
    {
        if (rankingObj.activeSelf) return;

        rankingObj.SetActive(true);

        for (int i = 0; i < context.childCount; i++)
        {
            Destroy(context.GetChild(i).gameObject);
        }

        GetLankingList(50, () => {
            for (int i = 0; i < tempUserInfoList.Count; i++)
            {
                GameObject prepab = Instantiate(userInfoPrepab, Vector3.zero, Quaternion.identity, context);
                prepab.transform.GetChild(0).GetComponent<Text>().text = "[" + tempUserInfoList[i].nickName + "]";
                prepab.transform.GetChild(1).GetComponent<Text>().text = tempUserInfoList[i].score + " second";
            }
        });
    }

    public void RnakingClose()
    {
        rankingObj.SetActive(false);
     
    }


    public GoogleSheetRanking sheetRanking;
    public void GetLankingList(int num, System.Action callback)
    {
        sheetRanking.GetUserList(() => {

            List<int> tempScore = new List<int>();
     

            for (int i = 0; i < sheetRanking.userInfoList.Count; i++)
            {
                tempScore.Add(sheetRanking.userInfoList[i].score);
            }
            tempScore.Sort(); tempScore.Reverse();

            int length = sheetRanking.userInfoList.Count;
            if (sheetRanking.userInfoList.Count > num) length = num;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < sheetRanking.userInfoList.Count; j++)
                {
                    if (sheetRanking.userInfoList[j].score == tempScore[i])
                    {
                        tempUserInfoList.Add(sheetRanking.userInfoList[j]);
                        break;
                    }
                }
            }

            callback();
        });
    }
}
