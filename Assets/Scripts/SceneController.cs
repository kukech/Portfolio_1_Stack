using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Tile lastTile;
    private Tile secondTile;
    private Vector3 camStartPosition;
    private Vector3 camSecondPosition;
    private Vector3 _velocity = Vector3.zero;
    private float maxDeltaToSuccessDrop = 0.05f;
    private void Awake()
    {
        Messenger<Action>.AddListener(GameEvent.TILE_DROP, DropTile);
        Messenger.AddListener(GameEvent.TILE_NEW, NewTile);
    }
    private void OnDestroy()
    {
        Messenger<Action>.RemoveListener(GameEvent.TILE_DROP, DropTile);
        Messenger.RemoveListener(GameEvent.TILE_NEW, NewTile);
    }
    void Start()
    {
        lastTile.enabled = false;
        camStartPosition = Camera.main.transform.position;
        camSecondPosition = camStartPosition;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, camSecondPosition, ref _velocity, Time.deltaTime);
    }
    public void DropTile(Action callback)
    {
        secondTile.enabled = false;
        Vector2 posLastTile = new Vector2(lastTile.transform.position.x, lastTile.transform.position.z); //without Y to calculate magnitude
        Vector2 posSecondTile = new Vector2(secondTile.transform.position.x, secondTile.transform.position.z);
        float deltaPos = (posLastTile - posSecondTile).magnitude * Mathf.Sign(posLastTile.x - posSecondTile.x);
        if (MainManager.Score % 2 == 0)
        {
            if (Mathf.Abs(deltaPos) >= lastTile.transform.localScale.z)
            {
                Messenger.Broadcast(GameEvent.GAME_OVER);
                MainManager.Instance.AddCrystall();
            }
            else if (Mathf.Abs(deltaPos) > maxDeltaToSuccessDrop)
            {
                secondTile.transform.Translate(Vector3.forward * deltaPos / 2);
                Vector3 scale = new Vector3(secondTile.transform.localScale.x,
                                            secondTile.transform.localScale.y,
                                            secondTile.transform.localScale.z - Mathf.Abs(deltaPos));
                secondTile.transform.localScale = scale;

                Tile lossTile = Instantiate(secondTile);
                lossTile.transform.Translate(-Vector3.forward * lastTile.transform.localScale.z / 2 * Mathf.Sign(deltaPos));
                scale.z = lastTile.transform.localScale.z - secondTile.transform.localScale.z;
                lossTile.transform.localScale = scale;
                lossTile.GetComponent<Rigidbody>().isKinematic = false;
                MainManager.Score++;
                callback();
                camSecondPosition.y += lastTile.transform.localScale.y;
                lastTile = secondTile;
                NewTile();
            }
            else
            {
                secondTile.transform.position = new Vector3(lastTile.transform.position.x, secondTile.transform.position.y, lastTile.transform.position.z);
                MainManager.Score++;
                callback();
                camSecondPosition.y += lastTile.transform.localScale.y;
                lastTile = secondTile;
                NewTile();
            }
        }
        else
        {
            if (Mathf.Abs(deltaPos) >= lastTile.transform.localScale.x)
            {
                Messenger.Broadcast(GameEvent.GAME_OVER);
                MainManager.Instance.AddCrystall();
            }
            else if (Mathf.Abs(deltaPos) > maxDeltaToSuccessDrop)
            {
                secondTile.transform.Translate(Vector3.right * deltaPos / 2);
                Vector3 scale = new Vector3(secondTile.transform.localScale.x - Mathf.Abs(deltaPos),
                                            secondTile.transform.localScale.y,
                                            secondTile.transform.localScale.z);
                secondTile.transform.localScale = scale;

                Tile lossTile = Instantiate(secondTile);
                lossTile.transform.Translate(-Vector3.right * lastTile.transform.localScale.x / 2 * Mathf.Sign(deltaPos));
                scale.x = lastTile.transform.localScale.x - secondTile.transform.localScale.x;
                lossTile.transform.localScale = scale;
                lossTile.GetComponent<Rigidbody>().isKinematic = false;
                MainManager.Score++;
                callback();
                camSecondPosition.y += lastTile.transform.localScale.y;
                lastTile = secondTile;
                NewTile();
            }
            else
            {
                secondTile.transform.position = new Vector3(lastTile.transform.position.x, secondTile.transform.position.y, lastTile.transform.position.z);
                MainManager.Score++;
                callback();
                camSecondPosition.y += lastTile.transform.localScale.y;
                lastTile = secondTile;
                NewTile();
            }
        }
       
    }
    public void NewTile()
    {
        Tile tile = Instantiate(lastTile);
        Vector3 pos = tile.transform.position;
        pos.y += tile.transform.localScale.y;
        tile.transform.position = pos;
        if (MainManager.Score % 2 == 0)
        {
            tile.transform.Translate(Vector3.forward * 3, Space.Self);
        }
        else
        {
            tile.transform.Translate(-Vector3.right * 3, Space.Self);
        }
        secondTile = tile;
        secondTile.enabled = true;
    }
}
