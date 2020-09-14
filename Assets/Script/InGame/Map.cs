using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    int row = 0;
    int maxLevel = 5;
    [HideInInspector] public Build[] buildKinds;
    IEnumerator[] resourceCoroutine;

    public Camera theCam;

    public Transform originMapTrans;
    Transform map;

    [Header("건물")]
    public GameObject house;
    public GameObject well;
    public GameObject farm;
    public GameObject electric;

    public void MapStart(int row)
    {
        map = originMapTrans;

        switch (row)
        {
            case 3:
                map = map.Find("3By3");
                break;
            case 4:
                map = map.Find("4By4");
                break;
            default:
                Debug.Log("해당 맵이 없습니다.");
                return;
        }

        this.row = row;
        int pow = row * row;
        buildKinds = new Build[pow];
        resourceCoroutine = new IEnumerator[pow];
        for (int i = 0; i < buildKinds.Length; i++)
        {
            buildKinds[i] = new Build();
        }
        SetBuild();
    }
    public bool IsSuccesBuild()
    {
        int iCount = 0;
        for (int i = 0; i < buildKinds.Length; i++)
        {
            if (buildKinds[i].level == 0)
            {
                iCount++;
            }
        }
        if (iCount == 0)
        {
            Debug.Log("생성 할 수 있는 장소가 없다.");
            return false;
        }
        return true;
    }
    public void CreateBuild(BuildKind buildKind, int level)
    {
        if (!IsSuccesBuild())
        {
            return;
        }

        int random = Random.Range(0, row * row);
        while (buildKinds[random].level != 0)
        {
            random = Random.Range(0, row * row);
        }

        buildKinds[random].buildKind = buildKind;
        buildKinds[random].level = level;

        SetBuild();

        resourceCoroutine[random] = ResourceCoroutine(buildKind, level);
        StartCoroutine(resourceCoroutine[random]);
    }
    public void DeleteBuild(int index)
    {
        buildKinds[index].buildKind = BuildKind.농장;
        buildKinds[index].level = 0;

        if (resourceCoroutine[index] != null)
        {
            StopCoroutine(resourceCoroutine[index]);
        }
    }
    public void MergeBuild(int index01, int index02)
    {
        // 건물과 레벨이 같다면 합병
        if (buildKinds[index01].buildKind == buildKinds[index02].buildKind
            && buildKinds[index01].level == buildKinds[index02].level)
        {
            BuildKind tempBuildKind = buildKinds[index01].buildKind;
            int tempLevel = buildKinds[index01].level + 1;

            DeleteBuild(index01);
            DeleteBuild(index02);

            CreateBuild(tempBuildKind, tempLevel);
        }

        SetBuild();
    }
    //건물 표시
    void SetBuild()
    {
        for (int i = 0; i < map.childCount; i++)
        {
            if (map.GetChild(i).childCount > 0)
            {
                for (int j = 0; j < map.GetChild(i).childCount; j++)
                {
                    Destroy(map.GetChild(i).GetChild(j).gameObject);
                }
            }
        }

        for (int i = 0; i < buildKinds.Length; i++)
        {
            if (buildKinds[i].level != 0)
            {
                GameObject buildObj = null;
                switch (buildKinds[i].buildKind)
                {
                    case BuildKind.집:
                        buildObj = house;
                        break;
                    case BuildKind.우물:
                        buildObj = well;
                        break;
                    case BuildKind.농장:
                        buildObj = farm;
                        break;
                    case BuildKind.발전소:
                        buildObj = electric;
                        break;
                }

                buildObj = Instantiate(buildObj, map.GetChild(i).position, Quaternion.identity, map.GetChild(i));
                buildObj.transform.localScale = new Vector3(3f / row, 3f / row, 3f / row);
                buildObj.GetComponent<Animator>().SetTrigger("level" + buildKinds[i].level);
                buildObj.transform.GetChild(0).GetComponent<Text>().text = "Lv " + buildKinds[i].level;
            }
        }
    }
    IEnumerator ResourceCoroutine(BuildKind buildKind, int level)
    {
        int time = (int)Time.instance.GetTime();
        WaitForSeconds wait = new WaitForSeconds(0.02f);
        while (true)
        {
            if ((int)Time.instance.GetTime() > time)
            {
                time++;
                InGame.instance.SetResource(buildKind, InGame.instance.GetResource(buildKind) + BuildParser.instance.GetBuidResouceIncrease(buildKind, level));
            }
            yield return wait;
        }
    }

    GameObject beingObj;
    bool isClick;
    int beingIndex;
    int endIndex;
    public void BeingClick(int index)
    {
        if (map.GetChild(index).childCount > 0)
        {
            if (buildKinds[index].level >= maxLevel)
            {
                return;
            }
            isClick = true;
            beingObj = map.GetChild(index).GetChild(0).gameObject;
            beingIndex = index;
        }
        else
        {
            return;
        }
    }

    public void EnterPoint(int index)
    {
        endIndex = index;
    }

    public void EndClick()
    {
        if (isClick && endIndex != beingIndex)
        {
            isClick = false;
            if (map.GetChild(endIndex).childCount > 0)
            {
                Debug.Log(map.GetChild(endIndex).childCount);
                MergeBuild(beingIndex, endIndex);
            }
            else
            {
                beingObj.transform.position = map.GetChild(beingIndex).position;
            }
        }
    }

    private void Update()
    {
        if (isClick)
        {
            Vector3 movePos = new Vector3(theCam.ScreenToWorldPoint(Input.mousePosition).x, theCam.ScreenToWorldPoint(Input.mousePosition).y, 0);
            beingObj.transform.position = movePos;
        }
    }
}

public class Build
{
    public BuildKind buildKind = BuildKind.농장;
    public int level = 0;

    public Build()
    {
        buildKind = BuildKind.농장;
        level = 0;
    }
}
