using UnityEngine;

namespace Yami {
    public class NormalBullet : MonoBehaviour, ITriggerable {
        [SerializeField]
        private Vector2 speed = new Vector2(400.0f, 0.0f);
        [SerializeField]
        private float maxDistance = 800.0f;

        private Transform objTransform;
        private Rigidbody2D objRigidbody;
        private Vector2 lastPosition = Vector2.zero;
        private float distance = 0.0f;

        public void Trigger(GameObject collider) {
            if (collider.tag != "Bullet") {
                Destroy(gameObject);
                Destroy(collider);
            }
        }

        void Awake() {
            // cache components
            objTransform = transform;
            objRigidbody = GetComponent<Rigidbody2D>();
            lastPosition = objTransform.position;
        }

        void Update() {
            UpdateMovement();
            UpdateDistance();
        }

        private void UpdateMovement() {
            Vector2 velocity = Vector2.zero;
            velocity.x = speed.x * Mathf.Sign(objTransform.localScale.x);
            velocity.y = speed.y;
            objRigidbody.velocity = GetRelativeVelocity(velocity);
        }

        private void UpdateDistance() {
            Vector2 currentPos = objTransform.position;
            distance += (currentPos - lastPosition).magnitude;
            lastPosition = currentPos;

            if (distance >= maxDistance) {
                Destroy(gameObject);
            }
        }

        private Vector2 GetRelativeVelocity(Vector2 velocity) {
            GameManager manager = GameManager.GetGameManager();
            return manager.GetRelativeVelocity(velocity);
        }
    }
}