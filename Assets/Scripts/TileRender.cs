using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRender
{
    public static void PaintTiles(HashSet<Vector2Int> positions, TileBase tile, Tilemap tilemap)
    {
        foreach (Vector2Int position in positions)
        {
            tilemap.SetTile((Vector3Int) position, tile);
        }
    }
}
