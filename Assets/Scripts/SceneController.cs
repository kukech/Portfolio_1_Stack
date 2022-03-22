using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Tile lastTile;
    private Tile secondTile;
    public static int Score { get; private set; }
    void Start()
    {
        lastTile.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DropTile()
    {
        secondTile.enabled = false;
        Vector2 posLastTile = new Vector2(lastTile.transform.position.x, lastTile.transform.position.z);
        Vector2 posSecondTile = new Vector2(secondTile.transform.position.x, secondTile.transform.position.z);
        float deltaPos = (posLastTile - posSecondTile).magnitude * Mathf.Sign(posLastTile.x - posSecondTile.x);
        if (Score % 2 == 0)
        {
            secondTile.transform.Translate(Vector3.forward * deltaPos / 2);
            Vector3 scale = new Vector3(secondTile.transform.localScale.x, 
                                        secondTile.transform.localScale.y, 
                                        secondTile.transform.localScale.z - Mathf.Abs(deltaPos));
            secondTile.transform.localScale = scale;

            Tile lossTile = Instantiate(secondTile);
            lossTile.transform.Translate(-Vector3.forward * lastTile.transform.localScale.z / 2 * Mathf.Sign(deltaPos));
            scale = secondTile.transform.localScale;
            scale.z = lastTile.transform.localScale.z - secondTile.transform.localScale.z;
            lossTile.transform.localScale = scale;
            lossTile.GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            secondTile.transform.Translate(Vector3.right * deltaPos / 2);
            Vector3 scale = new Vector3(secondTile.transform.localScale.x - Mathf.Abs(deltaPos), 
                                        secondTile.transform.localScale.y, 
                                        secondTile.transform.localScale.z);
            secondTile.transform.localScale = scale;

            Tile lossTile = Instantiate(secondTile);
            lossTile.transform.Translate(-Vector3.right * lastTile.transform.localScale.x / 2 * Mathf.Sign(deltaPos));
            scale = secondTile.transform.localScale;
            scale.x = lastTile.transform.localScale.x - secondTile.transform.localScale.x;
            lossTile.transform.localScale = scale;
            lossTile.GetComponent<Rigidbody>().isKinematic = false;
        }
        Score++;
        lastTile = secondTile;
        NewTile();
    }
    public void NewTile()
    {
        Tile tile = Instantiate(lastTile);
        Vector3 pos = tile.transform.position;
        pos.y += tile.transform.localScale.y;
        tile.transform.position = pos;
        if (Score % 2 == 0)
        {
            tile.transform.Translate(Vector3.forward * 3, Space.Self);
        }
        else
        {
            tile.transform.Translate(Vector3.right * 3, Space.Self);
        }
        secondTile = tile;
        secondTile.enabled = true;
    }
}
