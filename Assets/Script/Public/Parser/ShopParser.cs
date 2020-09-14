using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopParser : MonoBehaviour
{
    public static ShopParser instance;

    private void Awake()
    {
        instance = this;
    }

    public ShopParserStruct[] ShopList;

    private void Start()
    {
        int length = CSVParser.instance.Shop.GetLineLength() - 1;
        ShopList = new ShopParserStruct[length];
        for (int i = 0; i < length; i++)
        {
            List<string> data = CSVParser.instance.Shop.GetRowDataList(i + 1);
            ShopList[i].id = int.Parse(data[0]);
            ShopList[i].img = null;
            ShopList[i].title = data[2];
            ShopList[i].description = data[3];
            ShopList[i].ko_price = data[4];
            ShopList[i].us_price = data[5];
        }
    }
}
[System.Serializable]
public struct ShopParserStruct
{
    public int id;
    public Sprite img;
    public string title;
    public string description;
    public string ko_price;
    public string us_price;
}