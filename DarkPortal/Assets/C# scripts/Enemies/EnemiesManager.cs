using System.Linq;
using UnityEngine;

namespace C__scripts.Enemies
{
    public class EnemiesManager
    {
        private readonly Enemy[] enemies;
        private readonly Transform[] spawnPoints;

        private int currentSpawnPoint;
        private int size;

        public EnemiesManager(GameObject prefabEnemy, Transform[] spawnPoints, float radius, float speed)
        {
            size = spawnPoints.Length;
            this.spawnPoints = spawnPoints;
            enemies = new Enemy[size]
                .Zip(spawnPoints, (_, spawnPoint) => new Enemy(prefabEnemy, radius, speed, spawnPoint))
                .ToArray();
        }

        public void Update(Vector2 playerPosition)
        {
            if (currentSpawnPoint > size) return;
            if (currentSpawnPoint != size && 
                Vector2.Distance(spawnPoints[currentSpawnPoint].position, playerPosition) < 150f)
            {
                enemies[currentSpawnPoint].Born();
                currentSpawnPoint++;
            }
            else if (currentSpawnPoint > 0 && currentSpawnPoint <= size) enemies[currentSpawnPoint - 1].Update();
        }
    }
}