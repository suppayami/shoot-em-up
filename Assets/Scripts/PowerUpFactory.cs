using UnityEngine;

namespace Yami {
    [System.Serializable]
    public class PowerUpFactory {
        [SerializeField]
        private GameObject[] powerUpPrefabs;
        [SerializeField]
        private float baseSpawnTime = 3.0f;
        [SerializeField]
        private float randomRange = 1.5f;

        private float spawnTimeCount;

        public void SetupSpawn() {
            spawnTimeCount = GetRandomSpawnTime();
        }

        public void UpdateSpawn() {
            spawnTimeCount -= Time.deltaTime;
            if (spawnTimeCount <= 0) {
                spawnTimeCount += GetRandomSpawnTime();
                RandomSpawn();
            }
        }

        private void RandomSpawn() {
            GameManager manager = GameManager.GetGameManager();
            GameObject prefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
            Vector2 spawnPos = manager.GetRandomSpawnPosition();

            GameObject spwn = GameObject.Instantiate(prefab, spawnPos, Quaternion.identity);
            manager.AddObjectToWorld(spwn.transform);
        }

        private float GetRandomSpawnTime() {
            return baseSpawnTime + Random.Range(-randomRange, randomRange);
        }
    }
}