using UnityEngine;

namespace Yami {
    public class EnemyBasic : MonoBehaviour, IEnemy {
        private IMovable movement;
        private Transform playerTransform;
        private Transform objectTransform;

        public void UpdateAI() {
            Vector2 direction = (playerTransform.position - objectTransform.position).normalized;
            movement.Move(direction);
        }

        void Awake() {
            // cache
            movement = GetComponent<IMovable>();
            objectTransform = transform;

            GameManager manager = GameManager.GetGameManager();
            playerTransform = manager.GetPlayerObject().transform;
        }
    }
}