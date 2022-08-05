using System;
using System.Collections.Generic;
using Assets.Scripts.Infracructure.Services.AssetManagement;
using UnityEngine;

namespace Assets.Scripts.Infracructure.Logic
{
    public class Tower : MonoBehaviour
    {
        private const string FirstTilePointTag = "FirstTilePoint";
        private const float maxDeltaToSuccessDrop = 0.08f;

        public int PoolSize = 100;

        private List<GameObject> _tiles;
        private IAssets _assets;

        private int _activeTileIndex = 0;

        public GameObject ActiveTile => _tiles[_activeTileIndex];
        public Vector3 TowerCenterPos => new Vector3(
            _tiles[0].transform.position.x, 
            (_tiles[0].transform.position.y + ActiveTile.transform.position.y) / 2, 
            _tiles[0].transform.position.z);

        public float TowerHeight => _activeTileIndex * _tiles[0].transform.localScale.y;

        public void Construct(IAssets assets)
        {
            _assets = assets;
        }

        void Start()
        {
            _tiles = new List<GameObject>();

            TilesPooling();
        }

        public bool TryDropTile()
        {
            if (_activeTileIndex >= PoolSize)
                TilesPooling();

            if (_activeTileIndex >= 1)
            {
                if (_tiles[_activeTileIndex].TryDropTileOn(_tiles[_activeTileIndex - 1], maxDeltaToSuccessDrop))
                {
                    GetTile();
                    return true;
                }
                else return false;
            }

            GetTile();

            return true;
        }

        private void GetTile()
        {
            Vector3 offsetPos = ActiveTile.transform.position;
            offsetPos.y += ActiveTile.transform.localScale.y;
            Vector3 offsetScale = ActiveTile.transform.localScale;

            _activeTileIndex++;

            _tiles[_activeTileIndex].transform.localScale = offsetScale;
            _tiles[_activeTileIndex].transform.position = offsetPos;
            _tiles[_activeTileIndex].SetActive(true);
            //_tiles[_activeTileIndex].GetComponent<Tile>().enabled = true;
        }

        private void TilesPooling()
        {
            if (_tiles.Count > 1)
                PoolSize += 50;
            else InstanceFirstTile();

            for (int i = _tiles.Count; i <= PoolSize; i++)
            {
                GameObject tile = InstanceTile();
            }
        }

        private void InstanceFirstTile()
        {
            GameObject tile = _assets.Instantiate(AssetPath.TilePath);
            tile.transform.position = GameObject.FindWithTag(FirstTilePointTag).transform.position;
            tile.GetComponent<Tile>().enabled = false;

            _tiles.Add(tile);
        }

        private GameObject InstanceTile()
        {
            GameObject tile = _assets.Instantiate(AssetPath.TilePath);
            tile.SetActive(false);
            _tiles.Add(tile);

            return tile;
        }

        public void ResetTower()
        {
            foreach (GameObject tile in _tiles)
            {
                tile.SetActive(false);
            }

            _activeTileIndex = 0;
            _tiles[_activeTileIndex].SetActive(true);
        }
    }
}