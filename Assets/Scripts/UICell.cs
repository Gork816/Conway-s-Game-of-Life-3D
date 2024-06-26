using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICell : MonoBehaviour
{
    public bool status = false;
    [SerializeField]
    Image img;

    public void Click()
    {
        status = !status;
        ChangeColor();
    }

    public void ChangeColor()
    {
        if (status)
            img.color = Color.white;
        else
            img.color = Color.gray;
    }
}
