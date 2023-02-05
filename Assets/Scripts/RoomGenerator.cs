using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomGenerator
{
    public HashSet<Vector2Int> Generate(Vector2Int position, int iterations, int walkLength)
    {
        var currentPosition = position;
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < iterations; i++)
        {
            HashSet<Vector2Int> path = new HashSet<Vector2Int>();

            path.Add(currentPosition);
            var previousPosition = currentPosition;

            for (int w = 0; w < walkLength; w++)
            {
                var newPosition = previousPosition + DungeonGenerator.Direction2D.cardinalDirectionsList[Random.Range(0, DungeonGenerator.Direction2D.cardinalDirectionsList.Count)];
                path.Add(newPosition);
                previousPosition = newPosition;
            }
                
            roomPositions.UnionWith(path);
        }

        return roomPositions;
    }
}
