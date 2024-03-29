using UnityEngine;

namespace Yami {
    public class EnemyShoot : MonoBehaviour, IEnemy {
        private IMovable movement;
        private Transform playerTransform;
        private Transform objectTransform;
        private IGun gun;

        public void UpdateAI() {
            Vector2 direction = (playerTransform.position - objectTransform.position).normalized;
            movement.Move(direction);
            UpdateGun();
        }

        void Awake() {
            // cache
            movement = GetComponent<IMovable>();
            objectTransform = transform;
            gun = GetComponentInChildren<IGun>();

            GameManager manager = GameManager.GetGameManager();
            playerTransform = manager.GetPlayerObject().transform;
        }

        private void UpdateGun() {
            if (gun == null) {
                return;
            }

            gun.Shoot();
        }
    }
}