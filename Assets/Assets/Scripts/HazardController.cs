using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering.RendererUtils;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class HazardController : MonoBehaviour
{
    public GameObject hazardPrefab;
    public GameObject nonHazardPrefab;
    private GameObject luggageSprite;
    private GameObject luggageInspection;
    public PositionsInitialization positionsInitialization;
    public Vector3 luggageScale = new Vector3(1.5f, 1.5f, 1.5f);
    public Vector3 startingPosition = new Vector3(-19.6f, 0.8f, -0.2f);
    Vector3 itemScale = new Vector3(0.04f, 0.08f, 0);
    int hazardCounter;
    int deactivatedHazardsCounter;

    void Start()
    {
        hazardCounter = 0;
        deactivatedHazardsCounter = 0;
        luggageSprite = LuggageLogic();
        luggageInspection = LuggageInspectionLogic();
        PopulateLuggage();    
    }

    public void CreateLuggage()
    {
        luggageSprite = LuggageLogic();
        luggageInspection = LuggageInspectionLogic();
        PopulateLuggage();
    }

    public void DestroyLuggage()
    {
        Destroy(luggageSprite);
        Destroy(luggageInspection);
    }

    public void MoveLuggageToInspection()
    {
        luggageSprite.transform.position = new Vector3(-4.5f,-8.73f,-0.1f);
        luggageInspection.transform.position = new Vector3(-20, -20, 0.1f);

    }

    public void MoveLuggageToXray()
    {
        luggageInspection.transform.position = new Vector3(-8.69f, -6.76f, -0.2f);
        luggageSprite.transform.position = new Vector3(-15f,-15f,0);
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
        return Instantiate(hazardPrefab, new Vector3(-11,-2,-0.1f), Quaternion.identity);
    }

    GameObject CreateNonHazard()
    {
        return Instantiate(nonHazardPrefab, new Vector3(-9f, -2f, -0.1f), Quaternion.identity);
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
        Debug.Log(hazardCounter);
        Debug.Log(deactivatedHazardsCounter);
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
        renderer.sprite = GameAssets.i.LuggageInside1;

        luggageInspectionSprite.transform.localScale = luggageScale;
        luggageInspectionSprite.transform.localPosition = new Vector3(-20, -20, 0.1f);
        luggageInspectionSprite.transform.Rotate(0, 0, -90);

        return luggageInspectionSprite;
    }
}
