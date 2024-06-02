using UnityEngine;
using Random = System.Random;

namespace C__scripts.Enemies
{
    public class EnemiesManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject[] enemyPrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private Canvas canvasFight;
        [SerializeField] private Fight fight;
        [SerializeField] private bool isFullTimeSpawn;
        
        private GameObject[] enemies;
        private bool[] isSpawnEnemy;

        private int currentSpawnPoint;
        private int size;
        private Random random;

        public void Start()
        {
            size = spawnPoints.Length;
            enemies = new GameObject[size];
            random = new Random();
            isSpawnEnemy = new bool[size];
        }

        public void Update()
        {
            if (Vector3.Distance(spawnPoints[currentSpawnPoint].position, player.transform.position) < 20f)
            {
                if (!isFullTimeSpawn)
                {
                    enemies[currentSpawnPoint] = SpawnEnemy(spawnPoints[currentSpawnPoint], enemyPrefab[currentSpawnPoint]);
                    currentSpawnPoint++;
                    if (currentSpawnPoint >= size) 
                        Destroy(gameObject);
                }
                else
                {
                    if (spawnPoints[currentSpawnPoint].position.x - player.transform.position.x - 7f < 1e-5
                        && spawnPoints[currentSpawnPoint].position.x - player.transform.position.x > 0
                        && enemies[currentSpawnPoint] == null)
                        SpawnWithTime();
                }
            }
            
        }

        private void SpawnWithTime()
        {
            enemies[currentSpawnPoint] = SpawnEnemy(spawnPoints[currentSpawnPoint],
                enemyPrefab[random.Next(0, enemyPrefab.Length)]);
            isSpawnEnemy[currentSpawnPoint] = true;
            currentSpawnPoint++;
            if (currentSpawnPoint == spawnPoints.Length)
                currentSpawnPoint = 0;
        }

        private GameObject SpawnEnemy(Transform position, GameObject prefab)
        {
            var e = Instantiate(prefab);
            e.GetComponent<Enemy>().Init(e, position, player, canvasFight, fight, isFullTimeSpawn);
            return e;
        }
    }
}