using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private float maxDistance = 3f;
    [SerializeField] private float speed = 0.1f;
    void Update()
    {
        if(MainManager.Score % 2 == 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
        }
        if(transform.position.x > maxDistance && speed > 0)
        {
            speed = -speed;
        } else if(transform.position.x < -maxDistance && speed < 0)
        {
            speed = -speed;
        }
        if (transform.position.y < -10)
            Destroy(gameObject);
    }
}
