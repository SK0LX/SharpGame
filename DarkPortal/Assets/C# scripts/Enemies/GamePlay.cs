using UnityEngine;

namespace C__scripts.Enemies
{
    // review(30.06.2024): Не совсем понятно, как используется класс
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