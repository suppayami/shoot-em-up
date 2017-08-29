using UnityEngine;

namespace Yami {
    // Use on Renderer to make offset based on its parent game object
    public class PixelPerfect : MonoBehaviour {
        Transform parentTransform;
        Transform rendererTransform;

        void Awake() {
            rendererTransform = transform;
            parentTransform = rendererTransform.parent;
        }

        void Update() {
            Vector2 newPos = Vector2.zero;
            Vector2 parentPos = parentTransform.position;
            newPos.x = Mathf.Round(parentPos.x) - parentPos.x;
            newPos.y = Mathf.Round(parentPos.y) - parentPos.y;
            rendererTransform.localPosition = newPos;
        }
    }
}