using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

public class PatternEditor : MonoBehaviour
{
    UICell[,] UIcells = new UICell[30, 30];

    [SerializeField]
    GameObject uiCell;
    [SerializeField]
    GameObject frame;
    [SerializeField]
    GameObject window;

    int curY = 0;

    [SerializeField]
    TextMeshProUGUI textbox;

    [SerializeField]
    Evolution evo;
    [SerializeField]
    Controller ctrl;

    private void Start()
    {
        for (int x = 0; x < 30; x++)
            for (int z = 0; z < 30; z++)
            {
                GameObject newCell = Instantiate(uiCell, frame.transform);
                RectTransform rectTransform = newCell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(5 + 10 * x, 5 + 10 * z);
                UIcells[x, z] = newCell.GetComponent<UICell>();
            }

        GetLayer(curY);
    }

    private void GetLayer(int y)
    {
        for (int x = 0; x < 30; x++)
            for (int z = 0; z < 30; z++)
            {
                UIcells[x, z].status = evo.status[x, y, z];
                UIcells[x, z].ChangeColor();
            }
    }

    private void SendLayer(int y)
    {
        for (int x = 0; x < 30; x++)
            for (int z = 0; z < 30; z++)
            {
                evo.status[x, y, z] = UIcells[x, z].status;
                evo.CellSwitch(new Vector3Int(x, y, z));
            }
    }

    public void ChangeY(int dy)
    {
        if (curY + dy != -1 && curY + dy != 30)
        {
            SendLayer(curY);
            curY += dy;
            GetLayer(curY);
            textbox.text = "y = " + curY.ToString();
        }
    }
    
    public void CloseEditor()
    {
        SendLayer(curY);
        ctrl.ChangeCameraState();
        window.SetActive(false);
    }
}
