using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class NPCController : MonoBehaviour
{
    private GameObject travelerSprite;

    public float speed = 3;
    public float smoothTime = 0.5f;
    public Vector3 target = new Vector3(6.3f, -1.8f, 0);
    public Vector3 offScreenTarget = new Vector3(8.0f, -1.8f, 0);
    public Vector3 startingLocation = new Vector3(-19.6f, 0.8f, -0.1f);
    public Vector3 travelerScale = new Vector3(4.85f, 4.85f, 4.85f);



    Vector3 currentVelocity;
    private bool newTraveler;
    private BoxCollider2D boxCollider;

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
    }

    // public void MoveToPlayer()
    // {
    //     InvokeRepeating("Movement", 0f, 0.001f);
    // }

    // public void MoveOffScreen()
    // {
    //     transform.position = Vector3.SmoothDamp(transform.position, offScreenTarget, ref currentVelocity, smoothTime);
    //     Destroy(gameObject, 2);
    // }

    public void MoveToPlayerShit()
    {
        travelerSprite.transform.position += Vector3.right * 11.0f;
    }

    public void MoveOffScreenShit()
    {
        travelerSprite.transform.position += Vector3.right * 11.0f;
        DestroyTraveler();
    }

    public void DestroyTraveler()
    {
        Destroy(travelerSprite, 2);
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
        travelerSprite.tag = "NPC";
        setupBoxCollider();
        SpriteRenderer renderer = travelerSprite.AddComponent<SpriteRenderer>();
        renderer.sprite = GameAssets.i.TravelerSprite1;

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
