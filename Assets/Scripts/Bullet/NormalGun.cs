using UnityEngine;

namespace Yami {
    public class NormalGun : MonoBehaviour, IGun {
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

            GameManager manager = GameManager.GetGameManager();

            cooldown += shootCooldown;
            Vector2 padding = paddingBullet;
            Vector2 currentPos = objTransform.position;
            padding.x *= Mathf.Sign(objTransform.parent.localScale.x);

            Vector2 position = currentPos + padding;
            GameObject spawn = GameObject.Instantiate(bullet, position, Quaternion.identity);
            Transform spawnTransform = spawn.transform;
            manager.AddObjectToWorld(spawnTransform);

            Vector3 scale = spawnTransform.localScale;
            scale.x *= Mathf.Sign(objTransform.parent.localScale.x); // Gun is child of an object!
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