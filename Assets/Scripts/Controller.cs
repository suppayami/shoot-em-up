using UnityEngine;

namespace Yami {
    public class Controller : MonoBehaviour, IDestroyable {
        private IMovable movement;
        private Gun gun;

        public void Destroy() {
            gameObject.SetActive(false);
            Debug.Log("Game Over");
        }

        void Awake() {
            // cache components
            movement = GetComponent<IMovable>();
            gun = GetComponent<Gun>();
        }

        void Update() {
            movement.Move(GetDirection());
            UpdateGun();
        }

        void OnTriggerEnter2D(Collider2D collider) {
            ITriggerable triggerable = collider.GetComponent<ITriggerable>();
            if (triggerable != null) {
                triggerable.Trigger(gameObject);
            }
        }

        private void UpdateGun() {
            if (gun == null) {
                return;
            }

            if (IsFirePressed()) {
                gun.Shoot();
            }
        }

        private Vector2 GetDirection() {
            Vector2 result = new Vector2(
                Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")
            );
            return result;
        }

        private bool IsFirePressed() {
            return Input.GetButton("Jump");
        }
    }
}