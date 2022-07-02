using UnityEngine;

namespace Assets.Scripts.Infracructure.Logic
{
    public static class TileExtention
    {
        public static Vector3 AddY(this Vector3 vector, float y)
        {
            vector.y += y;
            return vector;
        }

        public static Vector3 Absolute(this Vector3 vector)
        {
            vector.x = Mathf.Abs(vector.x);
            vector.y = Mathf.Abs(vector.y);
            vector.z = Mathf.Abs(vector.z);
            return vector;
        }

        public static bool TryDropTileOn(this GameObject activeTile, GameObject previousTile, float maxDeltaToSuccessDrop)
        {
            Tile tile = activeTile.GetComponent<Tile>();
            tile.enabled = false;

            Vector3 deltaPos = SpacingBetweenTiles(activeTile, previousTile);
            deltaPos = activeTile.transform.InverseTransformDirection(deltaPos);

            if (IsFallCheck(previousTile, ref deltaPos))
            {
                tile.OnFall();
                tile.enabled = true;
                return false;
            }
            else if (deltaPos.magnitude < maxDeltaToSuccessDrop)
            {
                activeTile.transform.position = previousTile.transform.position.AddY(activeTile.transform.localScale.y);
                return true;
            }
            else
            {
                SliceTile(activeTile, previousTile, deltaPos);
            }

            return true;
        }

        private static bool IsFallCheck(GameObject previousTile, ref Vector3 deltaPos)
        {
            Vector3 offset = previousTile.transform.localScale - deltaPos.Absolute();

            return offset.x * offset.z < 0;
        }

        private static void SliceTile(GameObject activeTile, GameObject previousTile, Vector3 deltaPos)
        {
            Tile tile;
            activeTile.transform.Translate(-deltaPos / 2);
            activeTile.transform.localScale = activeTile.transform.localScale - deltaPos.Absolute();

            GameObject fallingTile = Object.Instantiate(activeTile);

            Vector3 deltaScale = Vector3.Scale(deltaPos.normalized, previousTile.transform.localScale);
            fallingTile.transform.Translate(deltaScale / 2);
            fallingTile.transform.localScale = previousTile.transform.localScale - (deltaScale.Absolute() - deltaPos.Absolute());

            tile = fallingTile.GetComponent<Tile>();
            tile.OnFall();
            tile.enabled = true;
        }

        private static Vector3 SpacingBetweenTiles(GameObject activeTile, GameObject previousTile)
        {
            Vector3 vector3 = activeTile.transform.position - previousTile.transform.position;
            vector3.y = 0;
            return vector3;
        }
    }
}
