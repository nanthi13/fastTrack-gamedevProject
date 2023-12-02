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

    void Awake()
    {
        //stateController = GetComponent<StateController>();
    }

    public void InspectButton()
    {
        if (stateController.GetState() == "handling")
        {
            hazardController.MoveLuggageToInspection();
            Debug.Log("Inspection button has been pressed");
            stateController.StateUpdate("inspecting");
        }
        else
        {
            Debug.Log("The inspection button is currently disabled");
        }
    }

    public void PassButton()
    {
        if (stateController.GetState() == "handling" || stateController.GetState() == "inspecting")
        {
            Debug.Log("Pass button has been pressed");
            npcController.MoveOffScreenShit();
            financeController.PlayerReward();
            hazardController.DestroyLuggage();
            npcController.CreateTraveler();
            hazardController.resetCounters();
            hazardController.CreateLuggage();
            stateController.StateUpdate("waiting");
            dayController.EndOfDay();
        }
        else
        {
            Debug.Log("Pass button is disabled");
        }
    }

    public void CallButton()
    {
        if (stateController.GetState() == "waiting")
        {
            // Here a new hazard object will be created. It will be responsible for making the traveler have hazards in their luggage or body
            Debug.Log("Call button has been pressed");
            npcController.MoveToPlayerShit();
            hazardController.MoveLuggageToXray();
            stateController.StateUpdate("handling");
            
        }
        else
        {
            Debug.Log("The call button is disabled");
        }
    }

    public void LuggageButton()
    {
        if (stateController.GetState() == "inspecting")
        {
            cameraController.EnableInspectionUI();
            Debug.Log("Luggage inspection buttno has been pressed");
        }
    }

    public void ReturnButton()
    {
        cameraController.EnableGameplayUI();
    }
}
