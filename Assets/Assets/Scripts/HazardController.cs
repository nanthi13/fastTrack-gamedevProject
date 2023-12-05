using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.RendererUtils;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class HazardController : MonoBehaviour
{
    public GameObject hazardPrefab;
    public GameObject nonHazardPrefab;

    public GameObject hazardPrefab1;
    public GameObject hazardPrefab2;
    public GameObject hazardPrefab3;
    public GameObject nonHazardPrefab1;
    public GameObject nonHazardPrefab2;
    public GameObject nonHazardPrefab3;
    public Sprite luggageInside;

    public GameObject metalHazardPrefab;
    private GameObject luggageSprite;
    private GameObject luggageInspection;
    public GameObject NpcCamera;
    public GameObject metalDetector;
    private GameObject npc;
    private GameObject oldLuggage;
    private GameObject oldInspectLuggage;

    public PositionsInitialization positionsInitialization;
    public Vector3 luggageScale = new Vector3(1.5f, 1.5f, 1.5f);

    public Vector3 startingPosition = new Vector3(-19.4f, -8.4f, -0.65f);
    public Vector3 callPosition = new Vector3(-8.6f, -8.4f, -0.65f);
    public Vector3 investPosition = new Vector3(-4.73f, -8.7f, -0.65f);

    public Vector3 investStartingPosition = new Vector3(-8.7f, -8.5f, -0.65f);
    public Vector3 investCallPosition = new Vector3(-8.64f, -5.92f, -0.65f);
    public Vector3 investInvestPosition = new Vector3(-25.27f, -14.78f, -0.65f);

    public Vector3 gonePosition = new Vector3(2f, -8.7f, -0.65f);
    Vector3 itemScale = new Vector3(0.09f, 0.11f, 0);
    private Vector3 mousePosition;

    public float moveSpeed = 4;
    public float smoothTime = 0.5f;

    private bool callVar;
    private bool investVar;
    private bool offScreenVar;

    int hazardCounter;
    int deactivatedHazardsCounter;
    
    
    

    void Start()
    {
        hazardCounter = 0;
        deactivatedHazardsCounter = 0;
        //luggageSprite = LuggageLogic();
        //luggageInspection = LuggageInspectionLogic();
        //PopulateLuggage();    
        //InitializeMetalHazard();
    }

    void Update()
    {
        if (NpcCamera.activeSelf == true)
        {
            metalDetector.SetActive(true);
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z += 1f;
            metalDetector.transform.position = Vector3.MoveTowards(metalDetector.transform.position, mousePosition, moveSpeed * Time.deltaTime);
            //Debug.Log("It should be following now");
        }
        else {
            metalDetector.SetActive(false);
        }

        if (callVar)
        {
            MoveLuggageToCall();
        }

        if (investVar)
        {
            MoveLuggageToInspection();
        }

        if (offScreenVar)
        {
            MoveLuggageOffScreen();
        }
    }

    public void CreateLuggage()
    {
        luggageSprite = LuggageLogic();
        luggageInspection = LuggageInspectionLogic();
        PopulateLuggage();
    }

    public void DestroyLuggage(GameObject luggage, GameObject inspectLuggage)
    {
        Destroy(luggage);
        Destroy(inspectLuggage);
    }

    public async void MoveLuggageToCall()
    {
        luggageSprite.transform.position = Vector3.MoveTowards(luggageSprite.transform.position, callPosition, moveSpeed * Time.deltaTime);
        await Task.Delay(2000);
        luggageInspection.transform.position = Vector3.MoveTowards(luggageInspection.transform.position, investCallPosition, moveSpeed * Time.deltaTime);
    }

    public async void MoveLuggageToInspection()
    {
        luggageInspection.transform.position = Vector3.MoveTowards(luggageInspection.transform.position, investStartingPosition, moveSpeed * Time.deltaTime);
        await Task.Delay(1000);
        luggageSprite.transform.position = Vector3.MoveTowards(luggageSprite.transform.position, investPosition, moveSpeed * Time.deltaTime);
        await Task.Delay(1000);
        luggageInspection.transform.position = investInvestPosition;

    }

    public async void MoveLuggageOffScreen()
    {
        oldInspectLuggage.transform.position = Vector3.MoveTowards(oldInspectLuggage.transform.position, investStartingPosition, moveSpeed * Time.deltaTime);
        await Task.Delay(1000);
        oldInspectLuggage.transform.position = gonePosition;
        oldLuggage.transform.position = Vector3.MoveTowards(oldLuggage.transform.position, gonePosition, moveSpeed * Time.deltaTime);
        
    }

    public async void CallMovementFunction()
    {
        callVar = true;
        await Task.Delay(2000);
        callVar = false;
    }

    public async void InvestMovementFunction()
    {
        investVar = true;
        await Task.Delay(2000);
        investVar = false;
    }

    public async void OffscreenMovementFunction()
    {
        oldLuggage = luggageSprite;
        oldInspectLuggage = luggageInspection;
        offScreenVar = true;
        await Task.Delay(3000);
        offScreenVar = false;
        DestroyLuggage(oldLuggage, oldInspectLuggage);
    }

    GameObject SpawnMetalHazard()
    {
        addHazardToCounter();
        return Instantiate(metalHazardPrefab, new Vector3(0,0,-0.1f), Quaternion.identity);
    }

    public void InitializeMetalHazard()
    {
        npc = GameObject.Find("NPC");

        GameObject hazard = SpawnMetalHazard();

        hazard.transform.parent = npc.transform;
        hazard.transform.localPosition = new Vector3(0,0,-0.23f);
        hazard.transform.localScale = itemScale;
    }

    void PopulateLuggage()
    {
        // This will need to be cleaned up and built up with some for loops when we implement random number of hazards
        Vector3[] positions = positionsInitialization.getPositions();

        GameObject obj1 = CreateRandomObject();
        GameObject obj2 = CreateRandomObject();
        GameObject obj3 = CreateRandomObject();
        GameObject obj4 = CreateRandomObject();
        GameObject obj5 = CreateRandomObject();

        obj1.transform.parent = luggageInspection.transform;
        obj2.transform.parent = luggageInspection.transform;
        obj3.transform.parent = luggageInspection.transform;
        obj4.transform.parent = luggageInspection.transform;
        obj5.transform.parent = luggageInspection.transform;

        obj1.transform.localPosition = positions[0];
        obj1.transform.Rotate(0,0,-90);
        obj1.transform.localScale = itemScale;

        obj2.transform.localPosition = positions[1];
        obj2.transform.Rotate(0,0,-90);
        obj2.transform.localScale = itemScale;

        obj3.transform.localPosition = positions[2];
        obj3.transform.Rotate(0,0,-90);
        obj3.transform.localScale = itemScale;

        obj4.transform.localPosition = positions[3];
        obj4.transform.Rotate(0,0,-90);
        obj4.transform.localScale = itemScale;

        obj5.transform.localPosition = positions[4];
        obj5.transform.Rotate(0,0,-90);
        obj5.transform.localScale = itemScale;

    }

    GameObject CreateHazard()
    {
        addHazardToCounter();
         int choice = Random.Range(0, 100);

        if (choice < 33)
        {
            return Instantiate(hazardPrefab1, new Vector3(-11,-2,-0.1f), Quaternion.identity);
        }
        else if (choice > 33 && choice < 66)
        {
            return Instantiate(hazardPrefab2, new Vector3(-11,-2,-0.1f), Quaternion.identity);
        }
        else
        {
            return Instantiate(hazardPrefab3, new Vector3(-11,-2,-0.1f), Quaternion.identity);
        }
        
    }

    GameObject CreateNonHazard()
    {
         int choice = Random.Range(0, 100);

        if (choice < 33)
        {
            return Instantiate(nonHazardPrefab1, new Vector3(-9f, -2f, -0.1f), Quaternion.identity);
        }
        else if (choice > 33 && choice < 66)
        {
            return Instantiate(nonHazardPrefab2, new Vector3(-9f, -2f, -0.1f), Quaternion.identity);
        }
        else
        {
            return Instantiate(nonHazardPrefab3, new Vector3(-9f, -2f, -0.1f), Quaternion.identity);
        }
        
    }

    GameObject CreateRandomObject()
    {
        int choice = Random.Range(0, 100);

        if (choice < 25)
        {
            return CreateHazard();
        }
        else
        {
            return CreateNonHazard();
        }
    }

    public void addHazardToCounter()
    {
        hazardCounter = hazardCounter + 1;
    }

    public void addDeactivatedHazardToCounter()
    {
        deactivatedHazardsCounter = deactivatedHazardsCounter + 1;
    }

    public void resetCounters()
    {
        hazardCounter = 0;
        deactivatedHazardsCounter = 0;
    }

    public bool checkDeactivatedHazards()
    {
        if(hazardCounter == deactivatedHazardsCounter)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    GameObject LuggageLogic()
    {
        GameObject luggageSprite = new GameObject("Luggage");
        SpriteRenderer renderer = luggageSprite.AddComponent<SpriteRenderer>();
        renderer.sprite = GameAssets.i.LuggageSprite1;

        luggageSprite.transform.localScale = luggageScale;
        luggageSprite.transform.localPosition = startingPosition;
        luggageSprite.transform.Rotate(0, 0, -90); 

        return luggageSprite;
    }

    GameObject LuggageInspectionLogic()
    {
        GameObject luggageInspectionSprite = new GameObject("LuggageInside");
        SpriteRenderer renderer = luggageInspectionSprite.AddComponent<SpriteRenderer>();
        renderer.sprite = luggageInside;

        luggageInspectionSprite.transform.localScale = luggageScale;
        luggageInspectionSprite.transform.localPosition = investStartingPosition;
        luggageInspectionSprite.transform.Rotate(0, 0, -90);

        return luggageInspectionSprite;
    }
}
