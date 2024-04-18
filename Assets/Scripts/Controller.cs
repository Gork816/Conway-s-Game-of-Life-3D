using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    bool isPaused = true;
    bool cameraOn = false;

    Evolution evo;
    [SerializeField]
    CameraControl cam;

    [SerializeField]
    GameObject pauseBtn;

    [SerializeField]
    Sprite pauseSpr;
    [SerializeField]
    Sprite resumeSpr;

    private void Start()
    {
        evo = GetComponent<Evolution>();
    }

    IEnumerator Tick()
    {
        if (!isPaused)
        {
            evo.Turn();
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Tick());
        }
    }

    public void PauseResume()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            StopCoroutine(Tick());
            pauseBtn.GetComponent<Image>().sprite = resumeSpr;
        } else
        {
            StartCoroutine(Tick());
            pauseBtn.GetComponent<Image>().sprite = pauseSpr;
        }
    }

    public void ChangeCameraState()
    {
        cameraOn = !cameraOn;
        cam.enabled = cameraOn;
    }
}
