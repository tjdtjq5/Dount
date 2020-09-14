using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    public Transform grid;

    public GameObject foodObj;
    public GameObject waterObj;
    public GameObject energyObj;

    public Map map;

    List<IEnumerator> resourceCoroutine = new List<IEnumerator>();
    List<BuildKind> build = new List<BuildKind>();

    int energyApearTime;

    public void ResourceStart()
    {
        AllDelete();

        energyApearTime = ConfigParser.instance.config.EnergyAppearTime_Sec;

        CreateResource(BuildKind.농장);
        CreateResource(BuildKind.우물);
    }
    public void AllDelete()
    {
        for (int i = 0; i < resourceCoroutine.Count; i++)
        {
            StopCoroutine(resourceCoroutine[i]);
        }

        for (int i = 0; i < grid.childCount; i++)
        {
            Destroy(grid.GetChild(i).gameObject);
        }

        build.Clear();
        resourceCoroutine.Clear();
    }

    void CreateResource(BuildKind buildKind)
    {
        if (build.Contains(buildKind))
        {
            return;
        }
        else
        {
            build.Add(buildKind);
            resourceCoroutine.Add(ResourceCoroutine(buildKind));
            StartCoroutine(resourceCoroutine[resourceCoroutine.Count - 1]);
        }
    }

    GameObject ResourceSet(BuildKind buildKind)
    {
        if (buildKind == BuildKind.집)
        {
            Debug.Log("집은 자원생성이 아닙니다.");
            return null;
        }

        GameObject obj = null;

        switch (buildKind)
        {
            case BuildKind.우물:
                obj = Instantiate(waterObj, Vector3.zero, Quaternion.identity, grid);
                break;
            case BuildKind.농장:
                obj = Instantiate(foodObj, Vector3.zero, Quaternion.identity, grid);
                break;
            case BuildKind.발전소:
                obj = Instantiate(energyObj, Vector3.zero, Quaternion.identity, grid);
                break;
        }

        return obj;
    }

    IEnumerator ResourceCoroutine(BuildKind buildKind)
    {
        GameObject obj = ResourceSet(buildKind);
        if (obj == null)
        {
            yield break;
        }

        int time = (int)Time.instance.GetTime();
        WaitForSeconds wait = new WaitForSeconds(0.02f);
        while (true)
        {
            if ((int)Time.instance.GetTime() > time)
            {
                time++;

                int cost = CostParser.instance.GetCost(time, buildKind);
                InGame.instance.SetResource(buildKind, InGame.instance.GetResource(buildKind) - cost);

                int current = InGame.instance.GetResource(buildKind);
                int max = InGame.instance.GetMaxResource(buildKind);
                float fillAmount = current / (float)max;
                obj.transform.Find("img").GetChild(0).GetComponent<Image>().DOFillAmount(fillAmount, 0.2f);

                for (int i = 0; i < grid.childCount; i++)
                {
                    bool isIncrease = IsUpResource(grid.GetChild(i).GetComponent<ResourcePrepab>().buildKind);
                    switch (isIncrease)
                    {
                        case true:
                            grid.GetChild(i).GetComponent<ResourcePrepab>().SetIncrease();
                            break;
                        case false:
                            grid.GetChild(i).GetComponent<ResourcePrepab>().SetDecrease();
                            break;
                    }
                }
            }
            yield return wait;
        }
    }

    private void Update()
    {
        if (!build.Contains(BuildKind.발전소))
        {
            if ((int)Time.instance.GetTime() >= energyApearTime)
            {
                CreateResource(BuildKind.발전소);
            }
        }
    }

    bool IsUpResource(BuildKind buildKind)
    {
        int increaseResource = 0;
        int decreaseResource = 0;
       
        for (int i = 0; i < map.buildKinds.Length; i++)
        {
            if (map.buildKinds[i] != null && map.buildKinds[i].buildKind == buildKind)
            {
                increaseResource += BuildParser.instance.GetBuidResouceIncrease(buildKind, map.buildKinds[i].level);
            }
        }

        decreaseResource = CostParser.instance.GetCost((int)Time.instance.GetTime(), buildKind);

        if (increaseResource < decreaseResource)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
