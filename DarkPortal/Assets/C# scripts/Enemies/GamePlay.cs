﻿using UnityEngine;

namespace C__scripts.Enemies
{
    public class GamePlay: MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private GameObject player;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private Canvas canvasFight;
        
        [SerializeField] private float radius;
        [SerializeField] private float speed;
        
        private EnemiesManager enemiesManager;

        void Start()
        {
            
        }
        
    }
}