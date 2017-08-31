using UnityEngine;

namespace Yami {
    public class Gun : MonoBehaviour {
        [SerializeField]
        private float shootCooldown = 0.2f;
        [SerializeField]
        private GameObject bullet;

        private float cooldown = 0.0f;
        private Transform objTransform;
        // instantiate bullet padded from the Gun
        private readonly Vector2 paddingBullet = new Vector2(24.0f, 0.0f);

        public void Shoot() {
            if (cooldown > 0.0f) {
                return;
            }

            cooldown += shootCooldown;
            Vector2 currentPos = objTransform.position;
            Vector2 padding = paddingBullet;
            padding.x *= Mathf.Sign(objTransform.localScale.x);

            Vector2 position = currentPos + padding;
            GameObject spawn = Instantiate(bullet, position, Quaternion.identity);
            Transform spawnTransform = spawn.transform;
            spawnTransform.parent = objTransform.parent;

            Vector3 scale = spawnTransform.localScale;
            scale.x *= Mathf.Sign(objTransform.localScale.x);
            spawnTransform.localScale = scale;

        }

        void Awake() {
            objTransform = transform;
        }

        void Update() {
            if (cooldown > 0.0f) {
                cooldown -= Time.deltaTime;
            }
        }
    }
}