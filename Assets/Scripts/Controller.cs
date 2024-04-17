using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    bool isPaused = true;

    Evolution evo;

    private void Start()
    {
        evo = GetComponent<Evolution>();
    }

    IEnumerator Tick()
    {
        if (!isPaused)
        {
            yield return new WaitForSeconds(1f);
            evo.Turn();
            Tick();
        }
    }

    void Pause()
    {
        StopCoroutine(Tick());
        isPaused = true;
    }

    void Resume()
    {
        StartCoroutine(Tick());
        isPaused = false;
    }
}
