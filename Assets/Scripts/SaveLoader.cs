using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoader : MonoBehaviour
{
    string syms = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    [SerializeField]
    Evolution evo;

    public void Save()
    {
        string res = "";

        foreach (var cell in evo.alive)
        {
            res += syms[cell.x];
            res += syms[cell.y];
            res += syms[cell.z];
        }

        TextEditor saver = new TextEditor();
        saver.text = res;
        saver.SelectAll();
        saver.Copy();
    }

    public void Paste()
    {
        evo.ClearMap();
        TextEditor paster = new TextEditor();
        paster.Paste();

        string map = paster.text;
        if (map.Length % 3 != 0)
            return;
        int[] coords = new int[map.Length];

        for (int i = 0; i < map.Length; i++)
        {
            int a = syms.IndexOf(map[i]);
            if (a == -1)
                return;
            coords[i] = a;
        }

        for (int i = 0; i < map.Length; i += 3)
        {
            int x = coords[i];
            int y = coords[i + 1];
            int z = coords[i + 2];

            evo.status[x, y, z] = true;
            evo.CellSwitch(new Vector3Int(x, y, z));
            evo.alive.Add(new Vector3Int(x, y, z));
        }
    }
}
