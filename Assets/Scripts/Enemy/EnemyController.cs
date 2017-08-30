using UnityEngine;

namespace Yami {
    public class EnemyController : MonoBehaviour, ITriggerable, IDestroyable {
        private IEnemy enemy;

        void Awake() {
            enemy = GetComponent<IEnemy>();
        }

        void Update() {
            enemy.UpdateAI();
        }

        void OnTriggerEnter2D(Collider2D collider) {
            ITriggerable triggerable = collider.GetComponent<ITriggerable>();
            if (triggerable != null) {
                triggerable.Trigger(gameObject);
            }
        }

        public void Trigger(GameObject collider) {
            if (collider.tag == "Player") {
                IDestroyable destroyable = collider.GetComponent<IDestroyable>();
                if (destroyable == null) {
                    return;
                }
                destroyable.Destroy();
            }
        }

        public void Destroy() {
            Destroy(gameObject);
        }
    }
}