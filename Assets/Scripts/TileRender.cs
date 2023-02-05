using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TileRender
{
    public static void PaintTiles(HashSet<Vector2Int> positions, TileBase tile, Tilemap tilemap)
    {
        foreach (Vector2Int position in positions)
        {
            tilemap.SetTile((Vector3Int) position, tile);
        }
    }
    
    public static void PaintRandomTiles(HashSet<Vector2Int> positions, TileBase[] tiles, Tilemap tilemap)
    {
        foreach (Vector2Int position in positions)
        {
            TileBase tile = tiles[Random.Range(0, tiles.Length)];
            tilemap.SetTile((Vector3Int) position, tile);
        }
    }
}
