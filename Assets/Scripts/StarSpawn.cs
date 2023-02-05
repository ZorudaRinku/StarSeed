using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StarSpawn
{
    public HashSet<Vector2Int> CalculatePositions(HashSet<Vector2Int> floorPositions)
    {
        HashSet<Vector2Int> positions = new HashSet<Vector2Int>();
        
        IEnumerable<Vector2Int> orderedFloorPositions = floorPositions.OrderBy(item => item.magnitude);
        orderedFloorPositions = orderedFloorPositions.Reverse();

        foreach (Vector2Int position in orderedFloorPositions)
        {
            if (positions.Count >= 15)
                return positions;
            
            // Check we have enough space
            int floorTiles = 0;
            int otherTiles = 0;
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (floorPositions.Contains(new Vector2Int(position.x + x, position.y + y)))
                        floorTiles++;
                    else
                        otherTiles++;
                }
            }
            
            if (otherTiles == 0 || floorTiles / otherTiles > 0.9f)
            {
                if (positions.Count == 0)
                {
                    positions.Add(position);
                    continue;
                }

                bool badSpawn = false;
                foreach (var starPosition in positions.ToList())
                {
                    float distance = Vector2Int.Distance(position, starPosition);
                    if (distance < 50)
                    {
                        badSpawn = true;
                        break;
                    }
                } 
                if (!badSpawn)
                    positions.Add(position);
                
            }
            
        }
        
        return positions;
    }
}
