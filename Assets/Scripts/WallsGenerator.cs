using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsGenator
{
    public HashSet<Vector2Int> Generate(HashSet<Vector2Int> floorPositions)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in DungeonGenerator.Direction2D.cardinalDirectionsList8D)
            {
                Vector2Int possibleWallPosition = new Vector2Int(position.x + direction.x, position.y + direction.y);
                if (!wallPositions.Contains(possibleWallPosition) && !floorPositions.Contains(possibleWallPosition))
                    wallPositions.Add(new Vector2Int(position.x + direction.x, position.y + direction.y));
            }
        }
        return wallPositions;
    }
}
