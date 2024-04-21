using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evolution : MonoBehaviour
{
    public bool[,,] status = new bool[30, 30, 30];
    int[,,] neighbours = new int[30, 30, 30];
    GameObject[,,] cells = new GameObject[30, 30, 30];

    public HashSet<Vector3Int> alive = new HashSet<Vector3Int>();
    //active cells = alive cells + their neighbours
    HashSet<Vector3Int> active = new HashSet<Vector3Int>();

    [SerializeField]
    GameObject cellObj;

    public int sMin = 4;
    public int sMax = 5;
    public int bMin = 5;
    public int bMax = 5;

    int Idx(int arg)
    {
        if (arg >= 0)
            return (arg % 30);
        else
            return (30 + arg);
    }

    public void CellSwitch(Vector3Int cell)
    {
        int x = cell.x;
        int y = cell.y;
        int z = cell.z;

        cells[x, y, z].SetActive(status[x, y, z]);
    }

    private void CellStatusUpdate(Vector3Int cell)
    {
        int x = cell.x;
        int y = cell.y;
        int z = cell.z;

        if (((bMin <= neighbours[x, y, z]) && (neighbours[x, y, z] <= bMax)) ||
            ((sMin <= neighbours[x, y, z]) && (neighbours[x, y, z] <= sMax) && (status[x, y, z])))
        {
            status[x, y, z] = true;
            alive.Add(cell);
        } else
        {
            status[x, y, z] = false;
        }

        CellSwitch(cell);
    }

    private void StatusUpdate()
    {
        foreach (var cell in active)
            CellStatusUpdate(cell);
        active.Clear();
    }

    private void CellAliveUpdate(Vector3Int cell)
    {
        int x = cell.x;
        int y = cell.y;
        int z = cell.z;
        neighbours[x, y, z] -= 1;
        //alive cell will interact with itself in the next loop

        //interacting with all neighbours
        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
                for (int dz = -1; dz <= 1; dz++) {
                    neighbours[Idx(x + dx), Idx(y + dy), Idx(z + dz)] += 1;
                    active.Add(new Vector3Int(Idx(x + dx), Idx(y + dy), Idx(z + dz)));
                }
    }

    private void AliveUpdate()
    {
        foreach (var cell in alive)
            CellAliveUpdate(cell);
        alive.Clear();
    }

    public void Turn()
    {
        AliveUpdate();
        StatusUpdate();

        for (int x = 0; x < 30; x++)
            for (int y = 0; y < 30; y++)
                for (int z = 0; z < 30; z++)
                    neighbours[x, y, z] = 0;
    }

    public void RefreshAlive()
    {
        alive.Clear();
        for (int x = 0; x < 30; x++)
            for (int y = 0; y < 30; y++)
                for (int z = 0; z < 30; z++)
                    if (status[x, y, z])
                        alive.Add(new Vector3Int(x, y, z));
    }

    private void Awake()
    {
        for (int x = 0; x < 30; x++)
            for (int y = 0; y < 30; y++)
                for (int z = 0; z < 30; z++)
                {
                    neighbours[x, y, z] = 0;
                    cells[x, y, z] = Instantiate(cellObj, new Vector3(x, y, z), Quaternion.identity);
                    cells[x, y, z].SetActive(status[x, y, z]);
                }
    }

    public void ClearMap()
    {
        for (int x = 0; x < 30; x++)
            for (int y = 0; y < 30; y++)
                for (int z = 0; z < 30; z++)
                {
                    status[x, y, z] = false;
                    CellSwitch(new Vector3Int(x, y, z));
                }

        alive.Clear();
    }
}
