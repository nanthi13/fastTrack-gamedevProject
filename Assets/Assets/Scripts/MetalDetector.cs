using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MetalDetector : MonoBehaviour
{
    float distance;
    MetalHazard metalHazard;
    public GameObject metalDetectorBlinker;
    private bool currentlyBlinking = false;
    
    
    void Start()
    {
        metalHazard = GameObject.Find("MetalHazard").GetComponent<MetalHazard>();
        metalDetectorBlinker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(this.gameObject.transform.position, metalHazard.transform.position);
        

        if (distance < 3 && distance > 2 && !currentlyBlinking)
        {
            StartCoroutine(Blinking(2));
        }
        else if (distance < 2 && distance > 1 && !currentlyBlinking)
        {
            StartCoroutine(Blinking(1.2f));
        }
        else if (distance < 1 && distance > 0 && !currentlyBlinking)
        {
            StartCoroutine(Blinking(0.7f));
        }
    }

    void BlinkingOn()
    {
        Debug.Log("Its should be blinking");
        metalDetectorBlinker.SetActive(true);
    }

    void BlinkingOff()
    {
        metalDetectorBlinker.SetActive(false);
    }

    private IEnumerator Blinking(float speed)
    {
        currentlyBlinking = true;
        BlinkingOff();
        yield return new WaitForSeconds(speed);
        BlinkingOn();
        currentlyBlinking = false;
        yield break;
    }

}
