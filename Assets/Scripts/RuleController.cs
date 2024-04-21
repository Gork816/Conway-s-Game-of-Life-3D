using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RuleController : MonoBehaviour
{
    [SerializeField]
    int[] rules = new int[4];

    [SerializeField]
    InputField[] inputs = new InputField[4];

    [SerializeField]
    Evolution evo;
    [SerializeField]
    GameObject window;
    [SerializeField]
    Controller ctrl;

    public void UpdateRules()
    {
        evo.sMin = rules[0];
        evo.sMax = rules[1];
        evo.bMin = rules[2];
        evo.bMax = rules[3];

        foreach (var btn in ctrl.buttons)
            btn.SetActive(true);

        window.SetActive(false);
        ctrl.ChangeCameraState();
    }

    public void RuleChange(int indx)
    {
        switch(indx)
        {
            case 0:
                rules[0] = Convert.ToInt32(inputs[0].text);
                if (rules[0] > rules[1])
                    inputs[1].text = Convert.ToString(rules[0]);
                break;
            case 1:
                rules[1] = Convert.ToInt32(inputs[1].text);
                if (rules[1] > 27)
                    inputs[1].text = Convert.ToString(27);
                if (rules[0] > rules[1])
                    inputs[0].text = Convert.ToString(rules[1]);
                break;
            case 2:
                rules[2] = Convert.ToInt32(inputs[2].text);
                if (rules[2] > rules[3])
                    inputs[3].text = Convert.ToString(rules[2]);
                break;
            case 3:
                rules[3] = Convert.ToInt32(inputs[3].text);
                if (rules[3] > 27)
                    inputs[3].text = Convert.ToString(27);
                if (rules[2] > rules[3])
                    inputs[2].text = Convert.ToString(rules[3]);
                break;
            default:
                break;
        }
    }
}
