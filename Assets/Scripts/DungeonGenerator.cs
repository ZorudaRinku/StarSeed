using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    public Vector2Int startPosition = Vector2Int.zero;

    public int iterations = 10;
    public int walkLength = 10;
    public bool startRandomlyEachIteration = true;

    public Tilemap floorTilemap, wallTilemap;
    public TileBase[] floorTiles, wallTiles = new TileBase[1];
    
    public GameObject Star;

    // Start is called before the first frame update
    private void Start()
    {
        RoomGenerator roomGenerator = new RoomGenerator();
        HashSet<Vector2Int> roomPositions = roomGenerator.Generate(Vector2Int.zero, iterations*4, walkLength);
        TileRender.PaintRandomTiles(roomPositions, floorTiles, floorTilemap);
        FloorGenerator floorGenerator = new FloorGenerator();
        HashSet<Vector2Int> floorPositions = floorGenerator.Generate(startPosition, iterations, walkLength, startRandomlyEachIteration);
        TileRender.PaintRandomTiles(floorPositions, floorTiles, floorTilemap);
        floorPositions.UnionWith(roomPositions);
        WallsGenator wallsGenerator = new WallsGenator();
        HashSet<Vector2Int> wallPositions = wallsGenerator.Generate(floorPositions);
        TileRender.PaintRandomTiles(wallPositions, wallTiles, wallTilemap);
        StarSpawn starSpawn = new StarSpawn();
        HashSet<Vector2Int> starPositions = starSpawn.CalculatePositions(floorPositions);
        PlaceStars(starPositions, Star);
    }

    private void PlaceStars(HashSet<Vector2Int> starPositions, GameObject star)
    {
        foreach (var position in starPositions)
        {
            Instantiate(star, new Vector3(position.x, position.y, 0), Quaternion.identity);
        }
    }

    public static class Direction2D
    {
        public static List<Vector2Int> cardinalDirectionsListWeightedSouth = new List<Vector2Int>
        {
            new(0, 1), // UP
            new(1, 0), // RIGHT
            new(0, -1), // DOWN
            new(0, -1), // DOWN
            new(-1, 0) // LEFT
        };
        public static List<Vector2Int> cardinalDirectionsListWeightedNorthWest = new List<Vector2Int>
        {
            new(0, 1), // UP
            new(0, 1), // UP
            new(1, 0), // RIGHT
            new(0, -1), // DOWN
            new(-1, 0), // LEFT
            new(-1, 0), // LEFT
            new(-1, 0) // LEFT
        };
        public static List<Vector2Int> cardinalDirectionsListWeightedNorthEast = new List<Vector2Int>
        {
            new(0, 1), // UP
            new(0, 1), // UP
            new(1, 0), // RIGHT
            new(1, 0), // RIGHT
            new(1, 0), // RIGHT
            new(0, -1), // DOWN
            new(-1, 0) // LEFT
        };
        
        public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
        {
            new(0, 1), // UP
            new(1, 0), // RIGHT
            new(0, -1), // DOWN
            new(-1, 0) // LEFT
        };
        
        public static List<Vector2Int> cardinalDirectionsList8D = new List<Vector2Int>
        {
            new(0, 1), // UP
            new(1, 1), // UP RIGHT
            new(1, 0), // RIGHT
            new(1, -1), // DOWN RIGHT
            new(0, -1), // DOWN
            new(-1, -1), // DOWN LEFT
            new(-1, 0), // LEFT
            new(-1, 1) // UP LEFT
        };
    }
}
