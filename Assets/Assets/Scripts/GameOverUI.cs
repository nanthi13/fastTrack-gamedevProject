using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{

    public GameObject gameOverMenu;

    // enable when lose condition is fulfilled
    // void on enable() {lose condition}
    private void OnEnable()
    {
        FinanceController.OnPlayerGameOver += EnableGameOverMenu;
    }


    // disable when lose condition is unfulfilled
    // private void OnDiable() {gameover condition}
    private void OnDisable()
    {
        FinanceController.OnPlayerGameOver -= EnableGameOverMenu;
    }


    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }
}
