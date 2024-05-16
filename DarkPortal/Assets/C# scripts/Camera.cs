using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    private bool fight;
    
    public Transform leftLimit1;
    public Transform rightLimit1;
    
    public Transform leftLimit2;
    public Transform rightLimit2;
    
    public Transform leftLimit3;
    public Transform rightLimit3;
    private int location;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        if (player.position.x < rightLimit1.position.x && player.position.x > leftLimit1.position.x)
            location = 1;
        if (player.position.x < rightLimit2.position.x && player.position.x > leftLimit2.position.x)
            location = 2;
        if (player.position.x < rightLimit3.position.x && player.position.x > leftLimit3.position.x)
            location = 3;
        
        if (fight == false)
        {
            switch (location)
            {
                case 1:
                    transform.position = CameraLocate(leftLimit1.position.x, rightLimit1.position.x);
                    break;
                case 2:
                    transform.position = CameraLocate(leftLimit2.position.x, rightLimit2.position.x);
                    break;
                case 3:
                    transform.position = CameraLocate(leftLimit3.position.x, rightLimit3.position.x);
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(leftLimit1.position.x, 0, 5), new Vector3(rightLimit1.position.x, 0, 5));
        
    }

    private Vector3 CameraLocate(float min, float max)
    {
        return new Vector3(
            Mathf.Clamp(player.position.x, min + 7, max - 7),
            player.position.y,
            -5);
    }
}


