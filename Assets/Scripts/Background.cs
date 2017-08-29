using UnityEngine;

namespace Yami {
    public class Background : MonoBehaviour {
        private MeshRenderer objRenderer;
        private Vector2 worldSize;

        void Awake() {
            objRenderer = GetComponentInChildren<MeshRenderer>();
            worldSize = GameManager.GetGameManager().GetWorldSize();
            objRenderer.sharedMaterial.SetTextureOffset("_MainTex", Vector2.zero);
        }

        void Update() {
            float x = GetPlayerVelocity().x * Time.deltaTime;
            Vector2 offset = objRenderer.sharedMaterial.GetTextureOffset("_MainTex");
            offset.x = Mathf.Repeat(offset.x + x / worldSize.x, 1.0f);
            objRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
        }

        private Vector2 GetPlayerVelocity() {
            GameManager manager = GameManager.GetGameManager();
            return manager.GetPlayerVelocity();
        }
    }
}