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

    int curY = 0;

    [SerializeField]
    Evolution evo;

    private void Start()
    {
        for (int x = 0; x < 30; x++)
            for (int z = 0; z < 30; z++)
            {
                GameObject newCell = Instantiate(uiCell, frame.transform);
                RectTransform rectTransform = newCell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(5 + 10 * x, 5 + 10 * z);
                UIcells[x, z] = GetComponent<UICell>();
            }

        //GetLayer(curY);
        StartTimer();
    }

    private void GetLayer(int y)
    {
        for (int x = 0; x < 30; x++)
            for (int z = 0; z < 30; z++)
            {
                UIcells[x, z].status = evo.status[x, y, z];
            }
    }

    private void SendLayer(int y)
    {
        for (int x = 0; x < 30; x++)
            for (int z = 0; z < 30; z++)
            {
                evo.status[x, y, z] = UIcells[x, z].status;
            }
    }

    public void ChangeY(int dy)
    {
        if (curY + dy != -1 && curY + dy != 30)
        {
            SendLayer(curY);
            curY += dy;
            GetLayer(curY);
        }
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(1f);
        GetLayer(curY);
    }
}
