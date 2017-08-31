using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Yami {
    public class Controller : MonoBehaviour, IDestroyable {
        [SerializeField]
        private GameObject defaultGun;

        private IGun gun;
        private IMovable movement;

        public void Destroy() {
            // gameObject.SetActive(false);
            GameManager manager = GameManager.GetGameManager();
            manager.GameOver();
        }

        public void SwitchGun(GameObject newGun) {
            if (gun != null) {
                // hax: Gun is always a MonoBehaviour
                GameObject gunObj = ((MonoBehaviour)gun).gameObject;
                Destroy(gunObj);
            }

            GameObject newGunObj = Instantiate(newGun, transform.position, Quaternion.identity);
            newGunObj.transform.parent = transform;
            gun = newGunObj.GetComponent<IGun>();
        }

        void Awake() {
            // cache components
            movement = GetComponent<IMovable>();
            gun = GetComponentInChildren<IGun>();

            if (defaultGun != null) {
                SwitchGun(defaultGun);
            }
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