using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Yami {
    public class Controller : MonoBehaviour, IDestroyable {
        private IMovable movement;
        private Gun gun;

        public void Destroy() {
            // gameObject.SetActive(false);
            GameManager manager = GameManager.GetGameManager();
            manager.GameOver();
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
#if MOBILE_INPUT
            Vector2 result = new Vector2(
                CrossPlatformInputManager.GetAxisRaw("Horizontal"),
                CrossPlatformInputManager.GetAxisRaw("Vertical")
            );
#else
            Vector2 result = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            );
#endif
            return result;
        }

        private bool IsFirePressed() {
#if MOBILE_INPUT
            return CrossPlatformInputManager.GetButton("Jump");
#else
            return Input.GetButton("Jump");
#endif
        }
    }
}