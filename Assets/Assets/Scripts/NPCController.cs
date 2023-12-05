using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class NPCController : MonoBehaviour
{
    private GameObject travelerSprite;
    public StateController stateController;
    public Sprite travelerCharacter;

    public float speed = 5;
    public float smoothTime = 0.5f;
    public Vector3 target = new Vector3(6.3f, -1.8f, 0);
    public Vector3 offScreenTarget = new Vector3(8.0f, -1.8f, -0.01f);
    public Vector3 startingLocation = new Vector3(-19.6f, 0.8f, -0.01f);
    public Vector3 travelerScale = new Vector3(4.85f, 4.85f, 4.85f);


    private GameObject oldTraveler;
    Vector3 currentVelocity;
    private bool newTraveler;
    private BoxCollider2D boxCollider;

    private bool movementVar;
    private bool offMovementVar;
    

    void Start()
    {
        travelerSprite = CreationLogic();

        //Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (newTraveler == true)
        {
            travelerSprite = CreationLogic();
            newTraveler = false;
        }

        if (movementVar)
        {
            MoveToPlayer();
        }

        if (offMovementVar)
        {
            MoveOffScreen();
        }
    }

    public void MoveToPlayer()
    {
        travelerSprite.transform.position = Vector3.MoveTowards(travelerSprite.transform.position, target, speed * Time.deltaTime);
    }

    public async void MoveOffScreen()
    {
        //GameObject oldTraveler = travelerSprite;
        oldTraveler.transform.position = Vector3.MoveTowards(oldTraveler.transform.position, offScreenTarget, speed * Time.deltaTime);
        await Task.Delay(2000);
        DestroyTraveler(oldTraveler);
    }

    public async void MovementFunction()
    {
        movementVar = true;
        await Task.Delay(2000);
        movementVar = false;
    }

    public async void OffMovementFunction()
    {
        oldTraveler = travelerSprite;
        offMovementVar = true;
        await Task.Delay(2000);
        offMovementVar = false;
    }

    public void DestroyTraveler(GameObject traveler)
    {
        Destroy(traveler, 2);
    }

    public void CreateTraveler()
    {
        newTraveler = true;
    }

    public void setupBoxCollider()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }
        boxCollider.size = new Vector2(1.0f, 1.0f);
        boxCollider.isTrigger = true;

    }

    GameObject CreationLogic()
    {
        GameObject travelerSprite = new GameObject("NPC");
        //travelerSprite.tag = "NPC";
        //setupBoxCollider();
        SpriteRenderer renderer = travelerSprite.AddComponent<SpriteRenderer>();
        renderer.sprite = travelerCharacter;

        travelerSprite.transform.localScale = travelerScale;
        travelerSprite.transform.position = startingLocation;

        return travelerSprite;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DialogueBox"))
        {
            Debug.Log("NPC has entered the dialogue box");
        }
    }

    void OnDrawGizmos()
    {
        // Visualize the Box Collider 2D
        if (boxCollider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
        }
    }
}
