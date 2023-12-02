using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class TestingScript : MonoBehaviour
{
    public float speed = 3;
    public float smoothTime = 0.5f;
    public Vector3 target = new Vector3(6.3f,-1.8f, 0);
    public Vector3 offScreenTarget = new Vector3(8.0f, -1.8f, 0);
    Vector3 currentVelocity;

    void Update()
    {
        //Move();
    }

    public void MoveToPlayer()
    {
        InvokeRepeating("Movement", 0f, 0.001f);
    }

    public void MoveOffScreen()
    {
        transform.position = Vector3.SmoothDamp(transform.position, offScreenTarget, ref currentVelocity, smoothTime);
        Destroy(gameObject, 2);
    }

    public void MoveToPlayerShit()
    {
        transform.position += Vector3.right * 12.0f;
    }

    public void MoveOffScreenShit()
    {
        transform.position += Vector3.right * 12.0f;
        Destroy(gameObject, 2);
    }
}
