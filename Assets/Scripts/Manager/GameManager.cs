using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

namespace Yami {
    public class GameManager : MonoBehaviour {
        [SerializeField]
        private GameState gameState;
        [SerializeField]
        private World world;
        [SerializeField]
        private EnemyFactory enemyFactory;
        [SerializeField]
        private PowerUpFactory powerUpFactory;
        [SerializeField]
        private Transform mainCamera;
        [SerializeField]
        private GameObject playerPrefab;
        [SerializeField]
        private GameObject powerupPrefab;
        [SerializeField]
        private Text scoreText;
        [SerializeField]
        private Text highScoreText;
        [SerializeField]
        private Text startText;
        [SerializeField]
        private Text gameOverText;
        [SerializeField]
        private Transform worldContainer;

        private STATE state = STATE.Start;
        private readonly string saveFilename = "save.bin";

        // STATE: current menu/state of game, used for game handlers
        enum STATE { Start, InGame, GameOver };


        /// <summary>
        /// Start the game at the beginning
        /// </summary>
        public void StartGame() {
            SetupGame();
            startText.gameObject.SetActive(false); // hide text
        }

        /// <summary>
        /// Replay the game after game over
        /// </summary>
        public void ReplayGame() {
            SetupGame();
            gameOverText.gameObject.SetActive(false); // hide text
        }

        /// <summary>
        /// Add score to current score. If score > highScore, change highScore to score.
        /// </summary>
        public void GameOver() {
            ResetObjects(); // reset all objects!
            if (gameState.score > gameState.highScore) {
                gameState.highScore = gameState.score;
                highScoreText.text = string.Format("Highscore: {0}", gameState.highScore);
                Save();
            }
            SetPlayerVelocity(Vector2.zero); // fixed die while moving
            gameOverText.gameObject.SetActive(true); // show game over text
            state = STATE.GameOver;
        }

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

        /// <summary>
        /// Add score to current score. If score > highScore, change highScore to score.
        /// </summary>
        public void AddScore(int score) {
            gameState.score += score;
            scoreText.text = string.Format("Score: {0}", gameState.score);
            if (gameState.score > gameState.highScore) {
                highScoreText.text = string.Format("Highscore: {0}", gameState.score);
            }
        }

        /// <summary>
        /// Add object to World Container for easy handling
        /// </summary>
        public void AddObjectToWorld(Transform objTransform) {
            objTransform.parent = worldContainer;
        }

        void Awake() {
            // Load savefile
            Load();

            startText.gameObject.SetActive(true);
        }

        void Update() {
            UpdateMenu();
            UpdateGame();
        }

        private void SetupGame() {
            ResetObjects();
            SetupPlayer();
            enemyFactory.SetupSpawn();
            powerUpFactory.SetupSpawn();
            gameState.score = 0; // reset score
            highScoreText.text = string.Format("Highscore: {0}", gameState.highScore);
            state = STATE.InGame;
        }

        private void ResetObjects() {
            // destroy all objects from previous plays
            int childrenCount = worldContainer.childCount;
            for (int i = childrenCount - 1; i >= 0; i--) {
                Destroy(worldContainer.GetChild(i).gameObject);
            }
        }

        private void SetupPlayer() {
            GameObject player = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
            player.transform.parent = worldContainer;
            world.SetPlayerObject(player);
            player.SetActive(true);
        }

        private void UpdateMenu() {
            if (state == STATE.InGame) {
                return;
            }

            if (!Input.anyKeyDown) {
                return;
            }

            if (state == STATE.Start) {
                StartGame();
            }

            if (state == STATE.GameOver) {
                ReplayGame();
            }
        }

        private void UpdateGame() {
            if (state != STATE.InGame) {
                return;
            }
            UpdateCamera();
            enemyFactory.UpdateSpawn();
            powerUpFactory.UpdateSpawn();
        }

        private void UpdateCamera() {
            Vector3 currentPos = mainCamera.position;
            Vector2 cameraOffset = world.GetCameraOffset();
            currentPos.x = cameraOffset.x;
            currentPos.y = cameraOffset.y;
            mainCamera.position = currentPos;
        }

        private void Save() {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream saveFile = File.Create(saveFilename);

            formatter.Serialize(saveFile, gameState);
            saveFile.Close();
        }

        private void Load() {
            if (!File.Exists(saveFilename)) {
                return;
            }

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream saveFile = File.Open(saveFilename, FileMode.Open);

            gameState = (GameState)formatter.Deserialize(saveFile);
            saveFile.Close();
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