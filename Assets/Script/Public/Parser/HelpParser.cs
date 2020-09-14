using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpParser : MonoBehaviour
{
    public static HelpParser instance;

    private void Awake()
    {
        instance = this;
    }

    public HelpParserStruct[] helpList;


    private void Start()
    {
        int length = CSVParser.instance.Help.GetLineLength() - 1;
        helpList = new HelpParserStruct[length];
        for (int i = 0; i < length; i++)
        {
            List<string> data = CSVParser.instance.Help.GetRowDataList(i + 1);
            helpList[i].id = int.Parse(data[0]);
            helpList[i].img = null;
            helpList[i].text = data[2];
        }
    }
}
[System.Serializable]
public struct HelpParserStruct
{
    public int id;
    public Sprite img;
    public string text;
}