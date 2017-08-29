using UnityEngine;

namespace Yami {
    public class GameManager : MonoBehaviour {
        [SerializeField]
        private GameState gameState;
        [SerializeField]
        private World world;
        [SerializeField]
        private Transform mainCamera;

        /// <summary>
        /// Sets Player's Velocity to be accessed by the other objects.
        /// Should be raw value (to be multiplied with Time.deltaTime)
        /// </summary>
        public void SetPlayerVelocity(Vector2 velocity) {
            world.SetPlayerVelocity(velocity);
        }

        /// <summary>
        /// Gets Player's Velocity, should be multiplied with Time.deltaTime
        /// for velocity per second.
        /// </summary>
        public Vector2 GetPlayerVelocity() {
            return world.GetPlayerVelocity();
        }

        /// <summary>
        /// Gets World Size, for relative positions calculating
        /// </summary>
        public Vector2 GetWorldSize() {
            return world.GetWorldSize();
        }

        /// <summary>
        /// Calculate camera offset when player moving.
        /// </summary>
        public void CalculateCameraOffset(Vector2 direction) {
            world.CalculateCameraOffset(direction);
        }

        void Update() {
            UpdateCamera();
        }

        void UpdateCamera() {
            Vector3 currentPos = mainCamera.position;
            Vector2 cameraOffset = world.GetCameraOffset();
            currentPos.x = cameraOffset.x;
            currentPos.y = cameraOffset.y;
            mainCamera.position = currentPos;
        }

        // makes thing global
        private static GameManager gameManager;
        public static GameManager GetGameManager() {
            if (!gameManager) {
                gameManager = GameObject.FindObjectOfType<GameManager>();
            }
            return gameManager;
        }
    }
}