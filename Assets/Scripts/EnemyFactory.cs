using UnityEngine;

namespace Yami {
    [System.Serializable]
    public class EnemyFactory {
        [SerializeField]
        private GameObject[] enemyPrefabs;
        [SerializeField]
        private float baseSpawnTime = 3.0f;

        private float currentSpawnTime;
        private float spawnTimeCount;

        public void SetupSpawn() {
            currentSpawnTime = baseSpawnTime;
            spawnTimeCount = Random.Range(0.0f, currentSpawnTime);
        }

        public void UpdateSpawn() {
            spawnTimeCount -= Time.deltaTime;
            if (spawnTimeCount <= 0) {
                spawnTimeCount += currentSpawnTime;
                RandomSpawn();
            }
        }

        private void RandomSpawn() {
            GameManager manager = GameManager.GetGameManager();
            GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Vector2 spawnPos = manager.GetRandomSpawnPosition();

            GameObject.Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }
}