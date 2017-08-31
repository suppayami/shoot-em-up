using UnityEngine;

namespace Yami {
    public class TripleGun : MonoBehaviour, IGun {
        [SerializeField]
        private float shootCooldown = 0.5f;
        [SerializeField]
        private GameObject bullet;

        private float cooldown = 0.0f;
        private Transform objTransform;
        // instantiate bullet padded from the Gun
        private readonly Vector2 paddingBullet = new Vector2(24.0f, 0.0f);
        private readonly Vector2 paddingBetweenBullet = new Vector2(0.0f, 16.0f);

        public void Shoot() {
            if (cooldown > 0.0f) {
                return;
            }

            GameManager manager = GameManager.GetGameManager();

            cooldown += shootCooldown;
            Vector2 padding = paddingBullet;
            Vector2 currentPos = objTransform.position;
            padding.x *= Mathf.Sign(objTransform.parent.localScale.x);

            for (int i = 0; i < 3; i++) {
                Vector2 position = currentPos + padding;

                if (i != 0) {
                    position += paddingBetweenBullet * Mathf.Pow(-1.0f, i);
                }

                GameObject spawn = GameObject.Instantiate(bullet, position, Quaternion.identity);
                Transform spawnTransform = spawn.transform;
                manager.AddObjectToWorld(spawnTransform);

                Vector3 scale = spawnTransform.localScale;
                // Gun is child of an object!
                scale.x *= Mathf.Sign(objTransform.parent.localScale.x);
                spawnTransform.localScale = scale;
            }
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