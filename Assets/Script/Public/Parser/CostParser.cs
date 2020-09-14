using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostParser : MonoBehaviour
{
    public static CostParser instance;

    private void Awake()
    {
        instance = this;
    }

    public CostParserStruct[] costList;

    public int GetCost(int currentTime ,BuildKind buildKind)
    {
        for (int i = 0; i < costList.Length; i++)
        {
            if (costList[i].Time_Sec >= currentTime)
            {
                switch (buildKind)
                {
                    case BuildKind.우물:
                        return costList[i].WaterCost;
                    case BuildKind.농장:
                        return costList[i].FoodCost;
                    case BuildKind.발전소:
                        return costList[i].EnergyCost;
                }
            }
        }
        return 0;
    }

    private void Start()
    {
        int length = CSVParser.instance.Cost.GetLineLength() - 1;
        costList = new CostParserStruct[length];
        for (int i = 0; i < length; i++)
        {
            List<string> data = CSVParser.instance.Cost.GetRowDataList(i + 1);
            costList[i].id = int.Parse(data[0]);
            costList[i].Time_Sec = int.Parse(data[1]);
            costList[i].FoodCost = int.Parse(data[2]);
            costList[i].WaterCost = int.Parse(data[3]);
            costList[i].EnergyCost = int.Parse(data[4]);
        }
    }
}
[System.Serializable]
public struct CostParserStruct
{
    public int id;
    public int Time_Sec;
    public int FoodCost;
    public int WaterCost;
    public int EnergyCost;
}