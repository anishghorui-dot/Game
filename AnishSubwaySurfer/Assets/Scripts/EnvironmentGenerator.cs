using UnityEngine;
using System.Collections.Generic;

public class EnvironmentGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] groundTilePrefabs;
    public GameObject[] obstaclePrefabs;
    public GameObject coinPrefab;
    public GameObject[] letterPrefabs; // A, N, I, S, H
    public GameObject[] buildingPrefabs;
    
    [Header("Generation Settings")]
    public Transform player;
    public float tileLength = 20f;
    public int numberOfTilesOnScreen = 5;
    public float spawnDistance = 100f;
    
    [Header("Spawn Chances")]
    [Range(0f, 1f)]
    public float obstacleSpawnChance = 0.4f;
    [Range(0f, 1f)]
    public float coinSpawnChance = 0.6f;
    [Range(0f, 1f)]
    public float letterSpawnChance = 0.2f;
    [Range(0f, 1f)]
    public float buildingSpawnChance = 0.5f;
    
    [Header("Lanes")]
    public float laneDistance = 3f;
    private int[] lanes = { -1, 0, 1 }; // Left, Middle, Right
    
    private List<GameObject> activeTiles = new List<GameObject>();
    private float nextSpawnZ = 0f;
    
    void Start()
    {
        // Spawn initial tiles
        for (int i = 0; i < numberOfTilesOnScreen; i++)
        {
            SpawnTile();
        }
    }
    
    void Update()
    {
        if (player.position.z - 35f > (nextSpawnZ - numberOfTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteOldTiles();
        }
    }
    
    void SpawnTile()
    {
        // Spawn ground tile
        GameObject tile;
        if (groundTilePrefabs.Length > 0)
        {
            GameObject tilePrefab = groundTilePrefabs[Random.Range(0, groundTilePrefabs.Length)];
            tile = Instantiate(tilePrefab, transform.forward * nextSpawnZ, Quaternion.identity);
        }
        else
        {
            // Create default tile if no prefabs
            tile = GameObject.CreatePrimitive(PrimitiveType.Cube);
            tile.transform.position = transform.forward * nextSpawnZ;
            tile.transform.localScale = new Vector3(10, 0.5f, tileLength);
            tile.tag = "Ground";
        }
        
        activeTiles.Add(tile);
        
        // Spawn obstacles
        if (Random.value < obstacleSpawnChance && obstaclePrefabs.Length > 0)
        {
            SpawnObstacle(nextSpawnZ);
        }
        
        // Spawn coins
        if (Random.value < coinSpawnChance)
        {
            SpawnCoins(nextSpawnZ);
        }
        
        // Spawn letters
        if (Random.value < letterSpawnChance && letterPrefabs.Length > 0)
        {
            SpawnLetter(nextSpawnZ);
        }
        
        // Spawn buildings (side decoration)
        if (Random.value < buildingSpawnChance && buildingPrefabs.Length > 0)
        {
            SpawnBuilding(nextSpawnZ);
        }
        
        nextSpawnZ += tileLength;
    }
    
    void SpawnObstacle(float zPosition)
    {
        int laneIndex = Random.Range(0, lanes.Length);
        float xPosition = lanes[laneIndex] * laneDistance;
        
        GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        GameObject obstacle = Instantiate(obstaclePrefab, 
            new Vector3(xPosition, 1f, zPosition + Random.Range(-5f, 5f)), 
            Quaternion.identity);
        obstacle.tag = "Obstacle";
    }
    
    void SpawnCoins(float zPosition)
    {
        int laneIndex = Random.Range(0, lanes.Length);
        float xPosition = lanes[laneIndex] * laneDistance;
        
        // Spawn a line of coins
        int coinCount = Random.Range(5, 15);
        for (int i = 0; i < coinCount; i++)
        {
            if (coinPrefab != null)
            {
                GameObject coin = Instantiate(coinPrefab,
                    new Vector3(xPosition, 1.5f, zPosition + i * 2f),
                    Quaternion.identity);
                coin.tag = "Coin";
            }
            else
            {
                // Create default coin
                GameObject coin = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                coin.transform.position = new Vector3(xPosition, 1.5f, zPosition + i * 2f);
                coin.transform.localScale = Vector3.one * 0.5f;
                coin.tag = "Coin";
                
                // Make it a trigger
                Collider coinCollider = coin.GetComponent<Collider>();
                if (coinCollider != null)
                {
                    coinCollider.isTrigger = true;
                }
                
                // Add yellow material
                Renderer renderer = coin.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Color.yellow;
                }
            }
        }
    }
    
    void SpawnLetter(float zPosition)
    {
        int laneIndex = Random.Range(0, lanes.Length);
        float xPosition = lanes[laneIndex] * laneDistance;
        
        int letterIndex = Random.Range(0, letterPrefabs.Length);
        
        if (letterPrefabs[letterIndex] != null)
        {
            GameObject letter = Instantiate(letterPrefabs[letterIndex],
                new Vector3(xPosition, 2f, zPosition + Random.Range(-5f, 5f)),
                Quaternion.identity);
            letter.tag = "ANISH_Letter";
            
            // Add LetterCollectible component
            LetterCollectible letterScript = letter.AddComponent<LetterCollectible>();
            letterScript.letterIndex = letterIndex;
        }
    }
    
    void SpawnBuilding(float zPosition)
    {
        // Spawn on sides
        float sidePosition = Random.value < 0.5f ? -7f : 7f;
        
        GameObject buildingPrefab = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
        Instantiate(buildingPrefab,
            new Vector3(sidePosition, 0f, zPosition + Random.Range(-5f, 5f)),
            Quaternion.identity);
    }
    
    void DeleteOldTiles()
    {
        if (activeTiles.Count > 0 && activeTiles[0].transform.position.z < player.position.z - 50f)
        {
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
        }
    }
}
