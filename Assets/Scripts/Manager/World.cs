using UnityEngine;

namespace Yami {
    [System.Serializable]
    public class World {
        [SerializeField]
        private Vector2 size = new Vector2(100.0f, 100.0f);
        [SerializeField]
        private Vector2 cameraOffsetMax = new Vector2(160.0f, 0.0f);
        [SerializeField]
        private Vector2 cameraOffsetAcceleration = new Vector2(320.0f, 0.0f);

        private Vector2 playerVelocity;
        private Vector2 cameraOffset = Vector2.zero;

        private GameObject playerObject;

        // don't want to spawn right @ the world bound
        private readonly Vector2 paddingSpawn = new Vector2(16, 16);

        public void SetPlayerObject(GameObject player) {
            playerObject = player;
        }

        public GameObject GetPlayerObject() {
            return playerObject;
        }

        public void SetPlayerVelocity(Vector2 velocity) {
            playerVelocity = velocity;
        }

        public Vector2 GetPlayerVelocity() {
            return playerVelocity;
        }

        public Vector2 GetWorldSize() {
            return size;
        }

        public Vector2 GetWorldPosition(Vector2 position) {
            Vector2 realPos = Vector2.zero;

            realPos.x = position.x;
            realPos.y = position.y;

            if (position.x < -size.x / 2) {
                realPos.x = size.x + position.x;
            }

            if (position.x > size.x / 2) {
                realPos.x = -size.x + position.x;
            }

            return realPos;
        }

        public Vector2 GetRelativeVelocity(Vector2 velocity) {
            return velocity - new Vector2(playerVelocity.x, 0.0f);
        }

        public Vector2 GetCameraOffset() {
            return cameraOffset;
        }

        public void CalculateCameraOffset(Vector2 direction) {
            Vector2 offsetTarget = Vector2.Scale(cameraOffsetMax, direction);
            Vector2 offset = VectorHelpers.MoveToward(
                cameraOffset, offsetTarget, cameraOffsetAcceleration * Time.deltaTime
            );
            cameraOffset = offset;
        }

        public Vector2 GetRandomSpawnPosition() {
            Vector2 pos = Vector2.zero;
            float maxX = size.x / 2 - paddingSpawn.x;
            float maxY = size.y / 2 - paddingSpawn.y;

            pos.x = Random.Range(-maxX, maxX);
            pos.y = Random.Range(-maxY, maxY);

            return pos;
        }
    }
}