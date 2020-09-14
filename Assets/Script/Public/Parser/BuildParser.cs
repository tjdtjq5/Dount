using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildParser : MonoBehaviour
{
    public static BuildParser instance;

    private void Awake()
    {
        instance = this;
    }

    public BuildParserStruct[] buildList;

    public int GetBuidResouceIncrease(BuildKind buildKind, int level)
    {
        for (int i = 0; i < buildList.Length; i++)
        {
            if (buildList[i].buildKind == buildKind && buildList[i].level == level)
            {
                return buildList[i].Increase;
            }
        }
        return 0;
    }

    private void Start()
    {
        int length = CSVParser.instance.Build.GetLineLength() - 1;
        buildList = new BuildParserStruct[length];
        for (int i = 0; i < length; i++)
        {
            List<string> data = CSVParser.instance.Build.GetRowDataList(i + 1);
            buildList[i].id = int.Parse(data[0]);
            buildList[i].buildKind = (BuildKind)System.Enum.Parse(typeof(BuildKind), data[1]);
            buildList[i].level = int.Parse(data[2]);
            buildList[i].Increase = int.Parse(data[3]);
        }
    }
}
[System.Serializable]
public struct BuildParserStruct
{
    public int id;
    public BuildKind buildKind;
    public int level;
    public int Increase;
}