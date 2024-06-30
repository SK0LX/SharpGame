using UnityEngine;
using Random = System.Random;

namespace C__scripts.Enemies
{
    // review(30.06.2024): Это больше похоже на EnemiesSpawner
    public class EnemiesManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject[] enemyPrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private Canvas canvasFight;
        [SerializeField] private Fight fight;
        [SerializeField] private bool isFullTimeSpawn;
        
        private GameObject[] enemies;

        private int currentSpawnPoint;
        private int size;
        private Random random;

        public void Start()
        {
            size = spawnPoints.Length;
            enemies = new GameObject[size];
            random = new Random();
        }

        public void Update()
        {
            // review(30.06.2024): Если инвертировать условие, то уменьшится вложенность
            if (Vector3.Distance(spawnPoints[currentSpawnPoint].position, player.transform.position) < 10f)
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
                    // review(30.06.2024): Такие сложные условия я бы выносил в методы (в плане один метод ShouldSpawnWithTime()
                    if (Vector3.Distance(spawnPoints[currentSpawnPoint].position, player.transform.position) > 5f
                        && player.transform.position.x < spawnPoints[currentSpawnPoint].position.x
                        && enemies[currentSpawnPoint] == null)
                        SpawnWithTime();
                }
            }
            
        }

        private void SpawnWithTime()
        {
            enemies[currentSpawnPoint] = SpawnEnemy(spawnPoints[currentSpawnPoint],
                enemyPrefab[random.Next(0, enemyPrefab.Length)]); // review(30.06.2024): Я бы вынес получение индекса в метод либо хотя бы в переменную, чтобы было проще читать
            // review(30.06.2024): можно упростить до currentSpawnPoint = (currentSpawnPoint + 1) % spawnPoints.Length;
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