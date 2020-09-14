using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopObj;

    public void ShopOpen()
    {
        shopObj.SetActive(true);
    }

    public void ShopClose()
    {
        shopObj.SetActive(false);
    }
}
