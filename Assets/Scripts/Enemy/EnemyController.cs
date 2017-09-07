using UnityEngine;

namespace Yami {
    public class EnemyController : MonoBehaviour, ITriggerable, IDestroyable {
        [SerializeField]
        private int score = 10;

        private IEnemy enemy;
        private bool isActivated = false;
        private SpriteRenderer objRenderer;

        public void Trigger(GameObject collider) {
            if (!isActivated) {
                return;
            }

            if (collider.tag == "Player") {
                IDestroyable destroyable = collider.GetComponent<IDestroyable>();
                if (destroyable == null) {
                    return;
                }
                destroyable.Destroy();
            }
        }

        public void Destroy() {
            GameManager manager = GameManager.GetGameManager();
            manager.AddScore(score);
            Destroy(gameObject);
        }

        void Awake() {
            enemy = GetComponent<IEnemy>();
            objRenderer = GetComponentInChildren<SpriteRenderer>();

            AwakeFadeIn();
        }

        void Update() {
            if (!isActivated) {
                return;
            }
            enemy.UpdateAI();
        }

        void FixedUpdate() {
            UpdateSpawn();
        }

        void OnTriggerEnter2D(Collider2D collider) {
            ITriggerable triggerable = collider.GetComponent<ITriggerable>();
            if (triggerable != null) {
                triggerable.Trigger(gameObject);
            }
        }

        private void AwakeFadeIn() {
            isActivated = false;
            Color color = objRenderer.color;
            color.a = 0.5f;
            color.r = 0.0f;
            color.g = 0.0f;
            color.b = 0.0f;
            objRenderer.color = color;
        }

        private void UpdateSpawn() {
            if (isActivated) {
                return;
            }

            Color color = objRenderer.color;
            color.r += 0.05f;
            color.g += 0.05f;
            color.b += 0.05f;
            if (color.r >= 1.0f) {
                color.a = 1.0f;
                isActivated = true;
            }

            objRenderer.color = color;
        }
    }
}