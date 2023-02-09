using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn
{
    public HashSet<Vector2Int> CalculatePositions(HashSet<Vector2Int> floorPositions)
    {
        HashSet<Vector2Int> positions = new();

        foreach (var position in floorPositions)
        {
            if (!positions.Contains(position))
            {
                if (Random.Range(0, 750) <= 1)
                {
                    positions.Add(position);
                }
            }
        }

        return positions;
    }
}
