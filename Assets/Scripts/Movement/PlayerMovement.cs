using UnityEngine;

namespace Yami {
    /// <summary>
    /// Player's movement handler. The movement is pseudo, the camera and
    /// player stay still while the others move relatively to player.
    /// </summary>
    public class PlayerMovement : MonoBehaviour, IMovable {
        [SerializeField]
        private Vector2 acceleration = new Vector2(200.0f, 400.0f);
        [SerializeField]
        private Vector2 maxSpeed = new Vector2(100.0f, 100.0f);

        private Transform objTransform;
        private Rigidbody2D objRigidbody;

        void Awake() {
            // cache components
            objTransform = transform;
            objRigidbody = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Makes changes to GameManager relative velocity, the other objects
        /// will move based on this value.
        /// </summary>
        public void Move(Vector2 direction) {
            Accelerate(direction);
            // horizontal and vertical movements different so we need
            // two methods.
            VerticalMove(direction);
            HorizontalMove(direction);
        }

        // smooth the movement
        private void Accelerate(Vector2 direction) {
            Vector2 currentVelocity = GetPlayerVelocity();
            Vector2 targetVelocity = Vector2.Scale(direction, maxSpeed);
            Vector2 velocity = VectorHelpers.MoveToward(
                currentVelocity, targetVelocity, acceleration * Time.deltaTime
            );

            SetPlayerVelocity(velocity);
        }

        private void VerticalMove(Vector2 direction) {
            Vector2 velocityVert = new Vector2(0, GetPlayerVelocity().y);
            objRigidbody.velocity = velocityVert;
        }

        private void HorizontalMove(Vector2 direction) {
            Vector3 scale = objTransform.localScale;

            if (GetPlayerVelocity().x > 0) {
                scale.x = Mathf.Abs(scale.x);
            }

            if (GetPlayerVelocity().x < 0) {
                scale.x = -Mathf.Abs(scale.x);
            }

            objTransform.localScale = scale;
            GameManager.GetGameManager().CalculateCameraOffset(direction);
        }

        private Vector2 GetPlayerVelocity() {
            GameManager manager = GameManager.GetGameManager();
            return manager.GetPlayerVelocity();
        }

        private void SetPlayerVelocity(Vector2 velocity) {
            GameManager manager = GameManager.GetGameManager();
            manager.SetPlayerVelocity(velocity);
        }
    }
}