using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVParser : MonoBehaviour
{
    public static CSVParser instance;

    private void Awake()
    {
        instance = this;
    }

    public CSV Build;
    public CSV Config;
    public CSV Cost;
    public CSV Event;
    public CSV Gold;
    public CSV Help;
    public CSV Shop;
    public CSV Text;

}
[System.Serializable]
public class CSV
{
    public TextAsset csvData;

    public int GetLineLength()
    {
        string[] dataRow = csvData.text.Split(new char[] {'\n'});
        return dataRow.Length - 1;
    }
    public List<string> GetRowDataList(int row)
    {
        List<string> dataList = new List<string>();
        string[] dataRow = csvData.text.Split(new char[] { '\n' });
        string[] data = dataRow[row].Split(',');
        for (int i = 0; i < data.Length; i++)
        {
            dataList.Add(data[i]);
        }
        return dataList;
    }
}
