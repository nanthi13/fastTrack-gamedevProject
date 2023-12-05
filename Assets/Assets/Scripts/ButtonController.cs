using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public NPCController npcController;
    public StateController stateController;
    public HazardController hazardController;
    public CameraController cameraController;
    public FinanceController financeController;
    public DayController dayController;


    public void InspectButton()
    {
        if (stateController.GetState() == "handling")
        {
            hazardController.InvestMovementFunction();
            //Debug.Log("Inspection button has been pressed");
            stateController.StateUpdate("inspecting");
        }
        else
        {
            //Debug.Log("The inspection button is currently disabled");
        }
    }

    public void PassButton()
    {
        if (stateController.GetState() == "handling" || stateController.GetState() == "inspecting")
        {
            //Debug.Log("Pass button has been pressed");
            npcController.OffMovementFunction();
            financeController.PlayerReward();
            hazardController.OffscreenMovementFunction();
            npcController.CreateTraveler();
            hazardController.resetCounters();
            
            stateController.StateUpdate("waiting");

            dayController.EndOfDay();
        }
        else
        {
            //Debug.Log("Pass button is disabled");
        }
    }

    public void CallButton()
    {
        if (stateController.GetState() == "waiting")
        {
            //hazardController.InitializeMetalHazard();
            hazardController.CreateLuggage();
            npcController.MovementFunction();
            hazardController.CallMovementFunction();
            stateController.StateUpdate("handling");
            
        }
        else
        {
            //Debug.Log("The call button is disabled");
        }
    }

    public void LuggageButton()
    {
        if (stateController.GetState() == "inspecting")
        {
            cameraController.EnableInspectionUI();
            //Debug.Log("Luggage inspection button has been pressed");
        }
    }

    public void NPCButton()
    {
        if (stateController.GetState() == "inspecting")
        {
            cameraController.EnableNpcUI();
        }
    }

    public void ReturnButton()
    {
        cameraController.EnableGameplayUI();
    }
}
