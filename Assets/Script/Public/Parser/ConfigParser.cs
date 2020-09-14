using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigParser : MonoBehaviour
{
    public static ConfigParser instance;

    private void Awake()
    {
        instance = this;
    }

    public ConfigParserStruct config;

    private void Start()
    {
        List<string> data = CSVParser.instance.Config.GetRowDataList(1);
        config.DefaultGold = int.Parse(data[0]);
        config.DefaultFood = int.Parse(data[1]);
        config.DefaultWater = int.Parse(data[2]);
        config.DefaultEnergy = int.Parse(data[3]);
        config.FoodMax = int.Parse(data[4]);
        config.WaterMax = int.Parse(data[5]);
        config.EnergyMax = int.Parse(data[6]);
        config.EnergyAppearType = int.Parse(data[7]);
        config.EnergyAppearTime_Sec = int.Parse(data[8]);
        config.EventNoticeTime_Sec = int.Parse(data[9]);
        config.RevivalFood = int.Parse(data[10]);
        config.RevivalWater = int.Parse(data[11]);
        config.RevivalEnergy = int.Parse(data[12]);
        config.BmStartTime_Sec = int.Parse(data[13]);
        config.BmStartGold = int.Parse(data[14]);
    }
}
[System.Serializable]
public struct ConfigParserStruct
{
    public int DefaultGold;
    public int DefaultFood;
    public int DefaultWater;
    public int DefaultEnergy;
    public int FoodMax;
    public int WaterMax;
    public int EnergyMax;
    public int EnergyAppearType;
    public int EnergyAppearTime_Sec;
    public int EventNoticeTime_Sec;
    public int RevivalFood;
    public int RevivalWater;
    public int RevivalEnergy;
    public int BmStartTime_Sec;
    public int BmStartGold;
}