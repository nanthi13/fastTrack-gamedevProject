using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class HazardScript : MonoBehaviour
{
    StateController stateController;
    HazardController hazardController;

    private bool deactivatedState;

    void Start()
    {
        stateController = GameObject.Find("StateController").GetComponent<StateController>();
        hazardController = GameObject.Find("HazardController").GetComponent<HazardController>();
        deactivatedState = false;
    }

    void OnMouseDown()
    {
        if(stateController.GetState() == "inspecting")
        this.gameObject.SetActive(false);
        Debug.Log("Hazard clicked");
        hazardController.addDeactivatedHazardToCounter();
    }

    public bool HazardState()
    {
        return deactivatedState;
    }

}
