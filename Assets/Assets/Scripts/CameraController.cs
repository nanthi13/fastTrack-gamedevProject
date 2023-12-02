using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject gameplayCamera;
    public GameObject inspectionCamera;
    public GameObject gameplayCanvas;
    public GameObject inspectionCanvas;
    

    public void EnableGameplayUI()
    {
        gameplayCamera.SetActive(true);
        gameplayCanvas.SetActive(true);
        inspectionCamera.SetActive(false);
        inspectionCanvas.SetActive(false);
    }

    public void EnableInspectionUI()
    {
        gameplayCamera.SetActive(false);
        gameplayCanvas.SetActive(false);
        inspectionCamera.SetActive(true);
        inspectionCanvas.SetActive(true);
    }
}
