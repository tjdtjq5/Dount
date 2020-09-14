using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextParser : MonoBehaviour
{
    public static TextParser instance;

    private void Awake()
    {
        instance = this;
    }

    public TextParserStruct[] textList;

    public string GetText(Nation nation, string stringkey)
    {
        for (int i = 0; i < textList.Length; i++)
        {
            if (textList[i].stringKey == stringkey)
            {
                switch (nation)
                {
                    case Nation.한국:
                        return textList[i].ko;
                    case Nation.미국:
                        return textList[i].us;
                }
            }
        }
        return "";
    }

    private void Start()
    {
        int length = CSVParser.instance.Text.GetLineLength() - 1;
        textList = new TextParserStruct[length];
        for (int i = 0; i < length; i++)
        {
            List<string> data = CSVParser.instance.Text.GetRowDataList(i + 1);
            textList[i].id = int.Parse(data[0]);
            textList[i].stringKey = data[1];
            textList[i].ko = data[2];
            textList[i].us = data[3];
        }
    }
}
[System.Serializable]
public struct TextParserStruct
{
    public int id;
    public string stringKey;
    public string ko;
    public string us;
}