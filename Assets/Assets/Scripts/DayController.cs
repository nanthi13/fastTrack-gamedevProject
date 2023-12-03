using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayController : MonoBehaviour
{
    int hours;
    int minutes;
    String meridien;

    public TextMeshProUGUI hourText;
    public TextMeshProUGUI minuteText;
    public TextMeshProUGUI MeridienText;
    
    SceneChanger sceneChanger;
    FinanceController financeController;


    // Start is called before the first frame update
    void Start()
    {
        sceneChanger = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
        financeController = GameObject.Find("FinanceController").GetComponent<FinanceController>();

        hours = 9;
        minutes = 00;
        meridien = "AM";

        InvokeRepeating("TrackTime", 0, 0.5f);
    }

    void TrackTime()
    {
        DisplayMinutes();
        DisplayHours();

        MeridienText.text = meridien;

        minutes = minutes + 5;
    }

    void DisplayHours()
    {
        if (hours > 12)
        {
            hourText.text = (hours - 12).ToString();
            meridien = "PM";
        }
        else
        {
            hourText.text = hours.ToString();
        }
    }

    void DisplayMinutes()
    {
        if (minutes < 10)
        {
            minuteText.text = "0" + minutes.ToString();
        }
        else if (minutes > 55)
        {
            minutes = 0;
            hours = hours + 1;
            minuteText.text = "0" + minutes.ToString();
        }
        else
        {
            minuteText.text = minutes.ToString();
        }
    }

    public void EndOfDay()
    {
        // Change this to when we want the day to end
        if (hours > 11)
        {
            financeController.FinishDayFinance();
            sceneChanger.ChangeToFamilyView();
        }
    }
}
