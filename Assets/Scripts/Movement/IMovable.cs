using UnityEngine;

namespace Yami {
    public interface IMovable {
        // Move the object toward direction
        void Move(Vector2 direction);
    }
}