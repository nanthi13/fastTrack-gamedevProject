using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class FinanceController : MonoBehaviour
{
    HazardController hazardController;
    public TextMeshProUGUI counterText;
    public static int money;
    public static int totalMoney;
    public static int numberPassed;
    public static int numberFailed;
    String sceneName;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();

        sceneName = currentScene.name;
        hazardController = GameObject.Find("HazardController").GetComponent<HazardController>();
    }

    public int GetScore()
    {
        return money;
    }

    public void FinishDayFinance()
    {
        totalMoney = totalMoney + money;
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    public static void ResetForNextDay()
    {
        money = 0;
        numberPassed = 0;
        numberFailed = 0;
    }

    public void PlayerReward()
    {
        if(hazardController.checkDeactivatedHazards() == true)
        {
            Debug.Log("YOU HAVE MISSED HAZARD");
            numberFailed++;
        }
        else
        {
            Debug.Log("No hazards missed. have moni");
            numberPassed++;
        }
    }
}
