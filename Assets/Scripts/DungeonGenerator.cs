using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
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
    public GameObject Enemy;

    public AstarPath astarPath;

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
        EnemySpawn enemySpawn = new EnemySpawn();
        HashSet<Vector2Int> enemySpawnPositions = enemySpawn.CalculatePositions(floorPositions);
        PlaceEnemy(enemySpawnPositions, Enemy);
        StartCoroutine(LateStart(0.1f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        astarPath.Scan();
    }

    private void PlaceStars(HashSet<Vector2Int> starPositions, GameObject star)
    {
        foreach (var position in starPositions)
        {
            Instantiate(star, new Vector3(position.x + 0.5f, position.y + 0.5f, -1), Quaternion.identity);
        }
    }


    private void PlaceEnemy(HashSet<Vector2Int> enemyPositions, GameObject enemy)
    {
        foreach (var position in enemyPositions)
        {
            GameObject enemyObject = Instantiate(enemy, new Vector3(position.x + 0.5f, position.y + 0.5f, 0), Quaternion.identity);
            enemyObject.GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
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
