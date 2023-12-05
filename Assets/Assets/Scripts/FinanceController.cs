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
    public static event Action OnPlayerGameOver;
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

    public void checkIfGameOver()
    {
        if (totalMoney < 1000)
        {
            OnPlayerGameOver?.Invoke();
        }
    }

    // public void FinishDayFinance1()
    // {
    //     totalMoney = totalMoney + money;
    //     // if money is less than x amount than invoke gameover menu
    //     // otherwise  proceed to next level
    //     if (totalMoney < 1000)
    //     {

    //         OnPlayerGameOver?.Invoke();
    //         Time.timeScale = 0f;


    //     }
    //     else
    //     {

    //         UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
    //         sceneName = currentScene.name;
    //     }
    // }

    public void FinishDayFinance()
    {
        totalMoney = totalMoney + money;
        // if money is less than x amount than invoke gameover menu
        // otherwise  proceed to next level


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
        if (hazardController.checkDeactivatedHazards() == true)
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
