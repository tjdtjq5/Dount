using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public GameObject SettingObj;

    public void SettingOpen()
    {
        SettingObj.SetActive(true);
    }
    public void SettingClose()
    {
        SettingObj.SetActive(false);
    }
}
