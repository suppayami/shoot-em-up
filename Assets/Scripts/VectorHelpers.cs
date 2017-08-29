using UnityEngine;

namespace Yami {
    public class VectorHelpers {
        public static Vector2 MoveToward(Vector2 current, Vector2 target, Vector2 acceleration) {
            Vector2 result = Vector2.zero;

            result.x = Mathf.MoveTowards(
                current.x, target.x, acceleration.x
            );
            result.y = Mathf.MoveTowards(
                current.y, target.y, acceleration.y
            );

            return result;
        }
    }
}