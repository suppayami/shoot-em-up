using UnityEngine;

namespace Yami {
    [System.Serializable]
    public class EnemyFactory {
        [SerializeField]
        private GameObject[] enemyPrefabs;
        [SerializeField]
        private float baseSpawnTime = 3.0f;
        [SerializeField]
        private float lowestSpawnTime = 1.0f;
        [SerializeField]
        private float decreaseSpawnTime = 0.05f; // decrease every second

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
            currentSpawnTime -= decreaseSpawnTime * Time.deltaTime;
            currentSpawnTime = Mathf.Max(currentSpawnTime, lowestSpawnTime);
        }

        private void RandomSpawn() {
            GameManager manager = GameManager.GetGameManager();
            GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Vector2 spawnPos = manager.GetRandomSpawnPosition();

            GameObject.Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }
}