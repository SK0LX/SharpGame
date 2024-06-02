using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;

    
    public Transform leftLimit1;
    public Transform rightLimit1;
    
    public Transform leftLimit2;
    public Transform rightLimit2;
    
    public Transform leftLimit3;
    public Transform rightLimit3;
    
    public Transform TavernaLeft;
    public Transform TavernaRight;
    
    public Transform farm1;
    public Transform farm2;
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
        if (player.position.x < TavernaRight.position.x && player.position.x > TavernaLeft.position.x)
            location = 4;
        
        if (player.position.x < farm2.position.x && player.position.x > farm1.position.x)
            location = 5;
        
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
            case 4:
                transform.position = CameraLocate(TavernaLeft.position.x, TavernaRight.position.x);
                break;
            case 5:
                transform.position = CameraLocate(farm1.position.x, farm2.position.x);
                break;
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


