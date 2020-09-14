using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventParser : MonoBehaviour
{
    public static EventParser instance;

    private void Awake()
    {
        instance = this;
    }

    public EventParserStruct[] eventList;


    private void Start()
    {
        int length = CSVParser.instance.Event.GetLineLength() - 1;
        eventList = new EventParserStruct[length];
        for (int i = 0; i < length; i++)
        {
            List<string> data = CSVParser.instance.Event.GetRowDataList(i + 1);
            eventList[i].id = int.Parse(data[0]);
            eventList[i].MinTime_Sec = int.Parse(data[1]);
            eventList[i].MaxTime_Sec = int.Parse(data[2]);
            eventList[i].FoodReduce = int.Parse(data[3]);
            eventList[i].WaterReduce = int.Parse(data[4]);
            eventList[i].EnergyReduce = int.Parse(data[5]);
            eventList[i].GoldReduce = int.Parse(data[6]);
        }
    }
}
[System.Serializable]
public struct EventParserStruct
{
    public int id;
    public int MinTime_Sec;
    public int MaxTime_Sec;
    public int FoodReduce;
    public int WaterReduce;
    public int EnergyReduce;
    public int GoldReduce;
}