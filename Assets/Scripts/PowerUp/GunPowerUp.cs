using UnityEngine;

namespace Yami {
    public class GunPowerUp : MonoBehaviour, ITriggerable {
        [SerializeField]
        private GameObject newGun;
        [SerializeField]
        private int score = 50;

        private Rigidbody2D objRigidbody;

        public void Trigger(GameObject collider) {
            if (collider.tag == "Player") {
                GameManager manager = GameManager.GetGameManager();
                manager.AddScore(score);

                Controller playerController = collider.GetComponent<Controller>();
                Destroy(gameObject);
                playerController.SwitchGun(newGun);
            }
        }

        void Awake() {
            // cache components
            objRigidbody = GetComponent<Rigidbody2D>();
        }

        void Update() {
            UpdateMovement();
        }

        private void UpdateMovement() {
            Vector2 velocity = Vector2.zero;
            objRigidbody.velocity = GetRelativeVelocity(velocity);
        }

        private Vector2 GetRelativeVelocity(Vector2 velocity) {
            GameManager manager = GameManager.GetGameManager();
            return manager.GetRelativeVelocity(velocity);
        }
    }
}