using UnityEngine;

namespace Yami {
    public interface Movable {
        // Move the object toward direction
        void Move(Vector2 direction);
    }
}