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

        public void SetPlayerVelocity(Vector2 velocity) {
            playerVelocity = velocity;
        }

        public Vector2 GetPlayerVelocity() {
            return playerVelocity;
        }

        public Vector2 GetWorldSize() {
            return size;
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
    }
}