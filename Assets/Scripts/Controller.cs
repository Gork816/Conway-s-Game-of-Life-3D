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

    public GameObject[] buttons = new GameObject[3];

    [SerializeField]
    Sprite pauseSpr;
    [SerializeField]
    Sprite resumeSpr;

    [SerializeField]
    GameObject editWin;
    [SerializeField]
    GameObject rulesWin;

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
            buttons[0].GetComponent<Image>().sprite = resumeSpr;
            for (int i = 1; i < buttons.Length; i++)
                buttons[i].SetActive(true);
        } else
        {
            StartCoroutine(Tick());
            buttons[0].GetComponent<Image>().sprite = pauseSpr;
            for (int i = 1; i < buttons.Length; i++)
                buttons[i].SetActive(false);
        }
    }

    public void ChangeCameraState()
    {
        cameraOn = !cameraOn;
        cam.enabled = cameraOn;
    }

    public void OpenEditor()
    {
        editWin.SetActive(true);
        edit.EditorOn();
        ChangeCameraState();
        foreach (var btn in buttons)
            btn.SetActive(false);
    }

    public void OpenRules()
    {
        rulesWin.SetActive(true);
        ChangeCameraState();
        foreach (var btn in buttons)
            btn.SetActive(false);
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
