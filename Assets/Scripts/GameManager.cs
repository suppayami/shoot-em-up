using UnityEngine;

namespace Yami {
    public class GameManager : MonoBehaviour {
        [SerializeField]
        private GameState gameState;
        [SerializeField]
        private World world;
        [SerializeField]
        private EnemyFactory enemyFactory;
        [SerializeField]
        private Transform mainCamera;

        /// <summary>
        /// Get Player Object. There's only one player on the Scene so meh~
        /// </summary>
        public GameObject GetPlayerObject() {
            return world.GetPlayerObject();
        }

        /// <summary>
        /// Set Player's Velocity to be accessed by the other objects.
        /// Should be raw value (to be multiplied with Time.deltaTime)
        /// </summary>
        public void SetPlayerVelocity(Vector2 velocity) {
            world.SetPlayerVelocity(velocity);
        }

        /// <summary>
        /// Get Player's Velocity, should be multiplied with Time.deltaTime
        /// for velocity per second.
        /// </summary>
        public Vector2 GetPlayerVelocity() {
            return world.GetPlayerVelocity();
        }

        /// <summary>
        /// Get World Size, for relative positions calculating
        /// </summary>
        public Vector2 GetWorldSize() {
            return world.GetWorldSize();
        }

        /// <summary>
        /// Get real position on the world. Use to deal with wrapped world.
        /// </summary>
        public Vector2 GetWorldPosition(Vector2 position) {
            return world.GetWorldPosition(position);
        }

        /// <summary>
        /// Get relative velocity based on player's velocity.
        /// </summary>
        public Vector2 GetRelativeVelocity(Vector2 velocity) {
            return world.GetRelativeVelocity(velocity);
        }

        /// <summary>
        /// Calculate camera offset when player moving.
        /// </summary>
        public void CalculateCameraOffset(Vector2 direction) {
            world.CalculateCameraOffset(direction);
        }

        /// <summary>
        /// Get random position on the game world to spawn a new object.
        /// </summary>
        public Vector2 GetRandomSpawnPosition() {
            return world.GetRandomSpawnPosition();
        }

        void Awake() {
            // Find Player Object for further uses
            GameObject playerObject = GameObject.Find("Player");
            world.SetPlayerObject(playerObject);
            enemyFactory.SetupSpawn();
        }

        void Update() {
            UpdateCamera();
            enemyFactory.UpdateSpawn();
        }

        private void UpdateCamera() {
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