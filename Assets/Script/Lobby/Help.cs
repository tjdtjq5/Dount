using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    public GameObject helpObj;

    public void HelpOpen()
    {
        helpObj.SetActive(true);
    }

    public void HelpClose()
    {
        helpObj.SetActive(false);
    }
}
