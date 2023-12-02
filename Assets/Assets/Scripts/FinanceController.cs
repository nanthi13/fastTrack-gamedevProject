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
    public TextMeshProUGUI savingsText;
    static int money;
    static int totalMoney;
    String sceneName;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();

        sceneName = currentScene.name;
        money = 0;
        hazardController = GameObject.Find("HazardController").GetComponent<HazardController>();
    }

    void Update()
    {
        if (sceneName == "GameplayScene")
        {
            counterText.text = money.ToString();
        }
        
        if (sceneName == "FamilyScene")
        {
            savingsText.text = "You have earned " + money.ToString() + " monis today";
            savingsText.text = "You have a a total of " + totalMoney.ToString() + " money";
        }
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

    public void PlayerReward()
    {
        if(hazardController.checkDeactivatedHazards() == true)
        {
            Debug.Log("YOU HAVE MISSED HAZARD");
        }
        else
        {
            Debug.Log("No hazards missed. have moni");
            money = money + 50;
        }
    }
}
