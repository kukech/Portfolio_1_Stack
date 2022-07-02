using UnityEngine;

namespace Assets.Scripts.Infracructure.Logic 
{
    public class Tile : MonoBehaviour
    {
        public float speed = 4f;
        public float maxDistance = 3f;

        private Vector3 direction;
        private bool isFalling = false;

        private void Start()
        {
            GenerateDirection();
        }

        void Update()
        {
            if (!isFalling)
            {
                TileMovement();
            }
            else
            {
                Fall();
            }
        }

        public void OnFall()
        {
            GetComponent<Rigidbody>().isKinematic = false;
            isFalling = true;
        }

        private void Fall()
        {
            if (transform.position.y < -10)
                Destroy(gameObject);
        }

        private void TileMovement()
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.Self);

            if (transform.position.x > maxDistance)
            {
                speed = -speed;
            }
            else if (transform.position.x < -maxDistance)
            {
                speed = -speed;
            }
        }
        private void GenerateDirection()
        {
            float randomValue = Random.value;
            if ((int)(randomValue * 10.0) % 2 == 0)
                direction = Vector3.forward * Mathf.Sign(randomValue - 0.5f);
            else
                direction = Vector3.right * Mathf.Sign(randomValue - 0.5f);
        }
    }
}