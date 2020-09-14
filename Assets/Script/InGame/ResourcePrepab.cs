using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePrepab : MonoBehaviour
{
    public BuildKind buildKind;
    public Sprite increaseSprite;
    public Sprite decreaseSprite;

    public Image[] changeImg;

    public void SetIncrease()
    {
        for (int i = 0; i < changeImg.Length; i++)
        {
            changeImg[i].sprite = increaseSprite;
        }
    }
    public void SetDecrease()
    {
        for (int i = 0; i < changeImg.Length; i++)
        {
            changeImg[i].sprite = decreaseSprite;
        }
    }
}
