using UnityEngine;
using System;

public class SceneController : MonoBehaviour, IObserver
{
    [SerializeField] private Tile lastTile;
    private Tile secondTile;

    private Camera mainCamera;


    private float maxDeltaToSuccessDrop = 0.05f;

    private Subject _subjects;
    private void Awake()
    {
        _subjects.Attach(GetComponent<UIController>());
    }
    void Start()
    {
        lastTile.enabled = false;
    }

    void LateUpdate()
    { 
    }
    private void DropTile()
    {
        secondTile.enabled = false;

        Vector2 posLastTile = new Vector2(lastTile.transform.position.x, lastTile.transform.position.z); //without Y to calculate magnitude
        Vector2 posSecondTile = new Vector2(secondTile.transform.position.x, secondTile.transform.position.z);
        float deltaPos = (posLastTile - posSecondTile).magnitude * Mathf.Sign(posLastTile.x - posSecondTile.x);

        if (UIController.Score % 2 == 0)
        {
            if (Mathf.Abs(deltaPos) >= lastTile.transform.localScale.z)
            {
                _subjects.state = GameEvent.GAME_OVER;
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
                lossTile.OnFall();
                lossTile.enabled = true;
                
                _subjects.state = GameEvent.SCORE_CHANGED;
            }
            else
            {
                secondTile.transform.position = new Vector3(lastTile.transform.position.x, secondTile.transform.position.y, lastTile.transform.position.z);
                _subjects.state = GameEvent.SCORE_CHANGED;
            }
        }
        else
        {
            if (Mathf.Abs(deltaPos) >= lastTile.transform.localScale.x)
            {
                _subjects.state = GameEvent.GAME_OVER;
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
                lossTile.OnFall();
                lossTile.enabled = true;

                _subjects.state = GameEvent.SCORE_CHANGED;
            }
            else
            {
                secondTile.transform.position = new Vector3(lastTile.transform.position.x, secondTile.transform.position.y, lastTile.transform.position.z);
                _subjects.state = GameEvent.SCORE_CHANGED;
            }
        }
        _subjects.Notify();
        if (_subjects.state != GameEvent.GAME_OVER)
        {
            lastTile = secondTile;
            NewTile();
        }
    }
    private void NewTile()
    {
        Tile tile = Instantiate(lastTile);
        Vector3 pos = tile.transform.position;
        pos.y += tile.transform.localScale.y;
        tile.transform.position = pos;
        if (UIController.Score % 2 == 0)
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

    public void UpdateData(GameEvent state)
    {
        if (state == GameEvent.TILE_DROP)
            DropTile();
        if (state == GameEvent.TILE_NEW)
            NewTile();
    }
}
