using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PositionsInitialization : MonoBehaviour
{
    Vector3[] luggage1 = new Vector3[] { new Vector3(-0.053f, -0.116f, -0.66f), new Vector3(-0.053f, -0.005f, -0.66f), new Vector3(-0.048f, 0.093f, -0.66f), new Vector3(0.042f, -0.103f, -0.66f), new Vector3(0.03f, 0.083f, -0.66f) };
    Vector3[] luggage2 = new Vector3[] { new Vector3(-0.053f, -0.005f, -0.66f), new Vector3(-0.048f, 0.093f, -0.66f), new Vector3(0.002f, -0.111f, -0.66f), new Vector3(0.064f, -0.119f, -0.66f), new Vector3(0.058f, 0f, -0.66f) };
    Vector3[] luggage3 = new Vector3[] { new Vector3(-0.053f, -0.109f, -0.66f), new Vector3(0.002f, -0.111f, -0.66f), new Vector3(-0.001f, -0.004f, -0.66f), new Vector3(0f, 0.091f, -0.66f), new Vector3(0.064f, -0.119f, -0.66f) };
    Vector3[] luggage4 = new Vector3[] { new Vector3(-0.053f, -0.109f, -0.66f), new Vector3(-0.046f, -0.006f, -0.66f), new Vector3(-0.045f, 0.091f, -0.66f), new Vector3(0.064f, -0.119f, -0.66f), new Vector3(0.05f, 0.091f, -0.66f) };
    Vector3[] luggage5 = new Vector3[] { new Vector3(-0.053f, -0.109f, -0.66f), new Vector3(-0.048f, -0.013f, -0.66f), new Vector3(-0.045f, 0.091f, -0.66f), new Vector3(0.002f, -0.11f, -0.66f), new Vector3(0.064f, -0.119f, -0.66f) };
    Vector3[] luggage6 = new Vector3[] { new Vector3(-0.048f, -0.013f, -0.66f), new Vector3(-0.045f, 0.091f, -0.66f), new Vector3(0.011f, -0.015f, -0.66f), new Vector3(0.064f, -0.119f, -0.66f), new Vector3(0.059f, 0.084f, -0.66f) };
    Vector3[][] luggageOptions;

    void Start()
    {
        luggageOptions = new Vector3[][] {
    luggage1,
    luggage2,
    luggage3,
    luggage4,
    luggage5,
    luggage6};
        Debug.Log(luggage1);

    }

    public Vector3[] getPositions()
    {
        int randomIndex = Random.Range(0, luggageOptions.Length - 1);
        // Debug.Log(luggageOptions[randomIndex]);
        return luggageOptions[randomIndex];
    }
}
