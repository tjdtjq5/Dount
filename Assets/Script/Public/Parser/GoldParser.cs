using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldParser : MonoBehaviour
{
    public static GoldParser instance;

    private void Awake()
    {
        instance = this;
    }

    public GoldParserStruct[] goldList;

    public int GetNeedGold(int count)
    {
        if (count < 0 || goldList.Length < count)
        {
            return 10000000;
        }
        return goldList[count].cost;
    }

    private void Start()
    {
        int length = CSVParser.instance.Gold.GetLineLength() - 1;
        goldList = new GoldParserStruct[length];
        for (int i = 0; i < length; i++)
        {
            List<string> data = CSVParser.instance.Gold.GetRowDataList(i + 1);
            goldList[i].id = int.Parse(data[0]);
            goldList[i].cost = int.Parse(data[1]);
        }
    }
}
[System.Serializable]
public struct GoldParserStruct
{
    public int id;
    public int cost;
}