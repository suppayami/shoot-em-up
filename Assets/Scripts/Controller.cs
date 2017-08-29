using UnityEngine;

namespace Yami {
    public class Controller : MonoBehaviour {
        Movable movement;

        void Awake() {
            // cache components
            movement = GetComponent<Movable>();
        }

        void Update() {
            movement.Move(GetDirection());
        }

        Vector2 GetDirection() {
            Vector2 result = new Vector2(
                Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")
            );
            return result;
        }
    }
}