using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalHazard : MonoBehaviour
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
        Destroy(gameObject);
    }

    public bool HazardState()
    {
        return deactivatedState;
    }
}
