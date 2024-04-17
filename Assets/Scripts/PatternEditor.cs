using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PatternEditor : MonoBehaviour
{
    UICell[,] UIcells = new UICell[30, 30];

    [SerializeField]
    GameObject uiCell;
    [SerializeField]
    GameObject frame;

    Evolution evo;

    private void Start()
    {
        //evo = GetComponent<Evolution>();
        for (int x = 0; x < 30; x++)
            for (int z = 0; z < 30; z++)
            {
                GameObject newCell = Instantiate(uiCell, frame.transform);
                RectTransform rectTransform = newCell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(5 + 10 * x, 5 + 10 * z);
                UIcells[x, z] = GetComponent<UICell>();
            }
    }

    private void GetLayer(int y)
    {
        for (int x = 0; x < 30; x++)
            for (int z = 0; z < 30; z++)
            {
                UIcells[x, z].status = evo.status[x, y, z];
            }
    }
}
