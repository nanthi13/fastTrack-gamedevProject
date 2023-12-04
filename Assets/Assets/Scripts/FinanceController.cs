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

        // change here
        if (sceneName == "FamilyScene")
        {
            savingsText.text = "You have earned " + money.ToString() + " monis today";
            savingsText.text = "You have a a total of " + totalMoney.ToString() + " money";
            if (totalMoney < 1000)
            {
                Debug.Log("gameover");

                OnPlayerGameOver?.Invoke();
                Time.timeScale = 0f;
            }

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

    public void FinishDayFinance1()
    {
        totalMoney = totalMoney + money;
        // if money is less than x amount than invoke gameover menu
        // otherwise  proceed to next level
        if (totalMoney < 1000)
        {

            OnPlayerGameOver?.Invoke();
            Time.timeScale = 0f;


        }
        else
        {

            UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
            sceneName = currentScene.name;
        }
    }

    public void FinishDayFinance()
    {
        totalMoney = totalMoney + money;
        // if money is less than x amount than invoke gameover menu
        // otherwise  proceed to next level


        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (totalMoney < 1000)
        {
            Debug.Log("gameover");

            OnPlayerGameOver?.Invoke();
            //Time.timeScale = 0f;
        }



    }

    public void PlayerReward()
    {
        if (hazardController.checkDeactivatedHazards() == true)
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
