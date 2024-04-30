using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    private bool fight;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    
    void LateUpdate()
    {
        if (fight == false)
            transform.position = new Vector3(player.position.x, player.position.y, -5);
    }
}

