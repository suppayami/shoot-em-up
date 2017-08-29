using UnityEngine;

namespace Yami {
    public class RelativeMovement : MonoBehaviour, Movable {
        public void Move(Vector2 direction) {

        }

        private Vector2 GetRelativeVelocity(Vector2 velocity) {
            GameManager manager = GameManager.GetGameManager();
            return velocity - manager.GetPlayerVelocity();
        }
    }
}