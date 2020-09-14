using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CreateBuild : MonoBehaviour
{
    int count = 1;
    public Map map;

    public void CreateBuildStart()
    {
        count = 1;
    }

    public void OnClickBuild()
    {
        int needGold = GoldParser.instance.GetNeedGold(count);
        int myGold = InGame.instance.GetResource(BuildKind.집);
        if (myGold < needGold)
        {
            Debug.Log("돈이 부족합니다.");
            Debug.Log("필요골드 : " + needGold + "  소지 골드 : " + myGold);
            return;
        }
        else
        {
            InGame.instance.SetResource(BuildKind.집 ,myGold - needGold);
        }

        int energyApearTime = ConfigParser.instance.config.EnergyAppearTime_Sec;
        int maxRandomBuildKind = 3;
        if (energyApearTime < Time.instance.GetTime())
        {
            maxRandomBuildKind = 4;
        }
        int randomBuildKind = Random.Range(0, maxRandomBuildKind);
        BuildKind buildKind = (BuildKind)System.Enum.ToObject(typeof(BuildKind), randomBuildKind);
        map.CreateBuild(buildKind, 1);

        MyGold.instance.TextSet();
    }
}
