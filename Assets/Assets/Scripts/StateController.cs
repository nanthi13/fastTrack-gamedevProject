using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateController : MonoBehaviour
{
    private string GameState = "waiting";

    // Start is called before the first frame update
    void Start()
    {
        //GameState = "awaiting";
    }

    public String GetState()
    {
        return GameState;
    }

    public void StateUpdate(string state)
    {
        GameState = state;
    }
}
