using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloorGenerator
{
    private HashSet<Vector2Int> floorPositions = new();

    public HashSet<Vector2Int> Generate(Vector2Int startPosition, int iterations, int walkLength, bool startRandomlyEachIteration)
    {
        floorPositions.UnionWith(RandomWalkPath(startPosition, iterations, walkLength, startRandomlyEachIteration, DungeonGenerator.Direction2D.cardinalDirectionsListWeightedSouth, false));
        floorPositions.UnionWith(RandomWalkPath(startPosition, iterations, walkLength, startRandomlyEachIteration, DungeonGenerator.Direction2D.cardinalDirectionsListWeightedSouth, true));
        floorPositions.UnionWith(RandomWalkPath(startPosition, iterations, walkLength, startRandomlyEachIteration, DungeonGenerator.Direction2D.cardinalDirectionsListWeightedNorthWest, false));
        floorPositions.UnionWith(RandomWalkPath(startPosition, iterations, walkLength, startRandomlyEachIteration, DungeonGenerator.Direction2D.cardinalDirectionsListWeightedNorthWest, true));
        floorPositions.UnionWith(RandomWalkPath(startPosition, iterations, walkLength, startRandomlyEachIteration, DungeonGenerator.Direction2D.cardinalDirectionsListWeightedNorthEast, false));
        floorPositions.UnionWith(RandomWalkPath(startPosition, iterations, walkLength, startRandomlyEachIteration, DungeonGenerator.Direction2D.cardinalDirectionsListWeightedNorthEast, true));

        return floorPositions;
    }

    private HashSet<Vector2Int> RandomWalkPath(Vector2Int startPosition, int iterations, int walkLength,
        bool startRandomlyEachIteration, List<Vector2Int> weightedDirections, bool generateRooms)
    {
        HashSet<Vector2Int> positions = new();
        var currentPosition = startPosition;
        for (int i = 0; i < iterations; i++)
        {
            HashSet<Vector2Int> path = new HashSet<Vector2Int>();

            path.Add(currentPosition);
            var previousPosition = currentPosition;

            for (int w = 0; w < walkLength; w++)
            {
                var newPosition = previousPosition + weightedDirections[Random.Range(0, weightedDirections.Count)];
                path.Add(newPosition);
                previousPosition = newPosition;
            }

            Vector2Int distance = previousPosition - startPosition;
            if (generateRooms && distance.magnitude > 100)
            {
                RoomGenerator roomGenerator = new RoomGenerator();

                path.UnionWith(roomGenerator.Generate(previousPosition, 25, 50));
            }
                
            positions.UnionWith(path);
            if (startRandomlyEachIteration)
                currentPosition = positions.ElementAt(Random.Range(0, positions.Count));
        }

        return positions;
    }
}
