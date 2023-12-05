using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneChanger : MonoBehaviour
{

    public void ChangeToFamilyView()
    {
        SceneManager.LoadScene("FamilyScene");
    }

    public void ChangeToGameplayView()
    {
        if (FinanceController.totalMoney > 0)
        {
            Debug.Log("loading gameplay");
            SceneManager.LoadScene("GameplayScene");

        }
        else
        {
            Debug.Log("loading gameover");
            SceneManager.LoadScene("GameoverScreen");
        }
    }

    public void ChangeToWinScreen()
    {
        SceneManager.LoadScene("WinScreen");
    }

    // public void ChangeToGameplayView()
    // {
    //     FinanceController.checkIfGameOver();


    //     Debug.Log("loading gameplay");
    //     SceneManager.LoadScene("GameplayScene");
    // }
}

