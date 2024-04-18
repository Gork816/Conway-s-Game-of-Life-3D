using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    bool isPaused = true;
    bool cameraOn = false;

    Evolution evo;
    [SerializeField]
    CameraControl cam;

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

    public void Pause()
    {
        StopCoroutine(Tick());
        isPaused = true;
    }

    public void Resume()
    {
        StartCoroutine(Tick());
        isPaused = false;
    }

    public void ChangeCameraState()
    {
        cameraOn = !cameraOn;
        cam.enabled = cameraOn;
    }
}
