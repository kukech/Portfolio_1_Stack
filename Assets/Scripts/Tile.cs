using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    private float maxDistance = 3f;

    private bool isFalling = false;
    void Update()
    {
        if (!isFalling)
        {
            TileMovement();
        }
        else
        {
            if (transform.position.y < -10)
                Destroy(gameObject);
        }
    }
    public void OnFall()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        isFalling = true;
    }
    private void TileMovement()
    {
        if (true)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
        }
        if (transform.position.x > maxDistance && speed > 0)
        {
            speed = -speed;
        }
        else if (transform.position.x < -maxDistance && speed < 0)
        {
            speed = -speed;
        }
    }
}
