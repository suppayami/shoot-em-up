using UnityEngine;

namespace Yami {
    // used for EnemyBasic
    public class EnemyBasicMovement : MonoBehaviour, IMovable {
        [SerializeField]
        private Vector2 acceleration = new Vector2(600.0f, 600.0f);
        [SerializeField]
        private float maxSpeed = 300.0f; // no need for a vector, since the AI changed the velocity
                                         // based on what it needs

        private Transform objTransform;
        private Rigidbody2D objRigidbody;
        private Vector2 absoluteVelocity = Vector2.zero;

        void Awake() {
            // cache components
            objTransform = transform;
            objRigidbody = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 direction) {
            Accelerate(direction);
            CalculateWorldPosition(direction);
        }

        private void Accelerate(Vector2 direction) {
            Vector2 currentVelocity = absoluteVelocity;
            Vector2 targetVelocity = direction * maxSpeed;
            Vector2 velocity = VectorHelpers.MoveToward(
                currentVelocity, targetVelocity, acceleration * Time.deltaTime
            );

            absoluteVelocity = velocity;
            objRigidbody.velocity = GetRelativeVelocity(absoluteVelocity);
        }

        private void CalculateWorldPosition(Vector2 direction) {
            GameManager manager = GameManager.GetGameManager();
            Vector2 currentPos = objTransform.position;
            objTransform.localPosition = manager.GetWorldPosition(currentPos);
        }

        private Vector2 GetRelativeVelocity(Vector2 velocity) {
            GameManager manager = GameManager.GetGameManager();
            return manager.GetRelativeVelocity(velocity);
        }
    }
}