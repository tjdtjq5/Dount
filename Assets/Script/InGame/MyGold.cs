using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGold : MonoBehaviour
{
    public static MyGold instance;

    private void Awake()
    {
        instance = this;
    }

    public Text goldText;
    public void TextSet()
    {
        goldText.text = "Gold : " + InGame.instance.GetResource(BuildKind.집);
    }
}
