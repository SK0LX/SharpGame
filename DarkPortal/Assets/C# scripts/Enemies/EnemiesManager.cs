using System.Collections.Generic;
using UnityEngine;

namespace C__scripts.Enemies
{
    public class EnemiesManager : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private GameObject player;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private Canvas canvasFight;
        [SerializeField] private Fight fight;
        
        [SerializeField] private float radius;
        [SerializeField] private float speed;
        
        private GameObject[] enemies;
        private List<GameObject> liveEnemies = new();

        private int currentSpawnPoint;
        private int size;

        public void Start()
        {
            size = spawnPoints.Length;
            enemies = new GameObject[size];
            for (var i = 0; i < size; i++)
            {
                var e = Instantiate(enemyPrefab);
                var comp = e.GetComponent<Enemy>();
                comp.Init(e, radius, speed, spawnPoints[i], player, canvasFight, fight);
                enemies[i] = e;
            }
            liveEnemies = new();
        }

        public void Update()
        {
            foreach (var enemy in liveEnemies)
                if (enemy.GetComponent<Enemy>().State is EnemyState.Die)
                    liveEnemies.Remove(enemy);
            
            if (currentSpawnPoint >= size) return;
            
            if (enemies?[currentSpawnPoint].GetComponent<Enemy>().State is EnemyState.None &&
                Vector3.Distance(spawnPoints[currentSpawnPoint].position, player.transform.position) < 50f)
            {
                liveEnemies.Add(enemies[currentSpawnPoint]);
                currentSpawnPoint++;
            }
        }
    }
}