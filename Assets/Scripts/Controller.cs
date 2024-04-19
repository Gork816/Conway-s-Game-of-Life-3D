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
    PatternEditor edit;

    [SerializeField]
    public GameObject pauseBtn;
    [SerializeField]
    GameObject editBtn;

    [SerializeField]
    Sprite pauseSpr;
    [SerializeField]
    Sprite resumeSpr;

    [SerializeField]
    GameObject editWin;

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
            editBtn.SetActive(true);
        } else
        {
            StartCoroutine(Tick());
            pauseBtn.GetComponent<Image>().sprite = pauseSpr;
            editBtn.SetActive(false);
        }
    }

    public void ChangeCameraState()
    {
        cameraOn = !cameraOn;
        cam.enabled = cameraOn;
    }

    public void OpenEditor()
    {
        editBtn.SetActive(false);
        editWin.SetActive(true);
        pauseBtn.SetActive(false);
        edit.EditorOn();
        ChangeCameraState();
    }
}
