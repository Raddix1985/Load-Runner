using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    // Variables
    public GameObject[] tilePrefabs;

    private Transform playerTransform;
    private float spawnZ = -18.0f;
    private float tileLength = 15.0f;
    private int amountOfTiles = 7;
    private float safeZone = 18.0f;
    private int lastPrefabIndex = 0;

    // Tiles prefab
    private List<GameObject> activeTiles;

    
    void Start()
    {
        // Spawn tiles as player moves
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Number of tiles to spawn
        for (int i = 0; i < amountOfTiles; i++)
        {
            if (i < 2)
                SpawnTile(0);
            else
                SpawnTile();
           
        }
      
    }

    // Set spawn and delete tile method
    void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - amountOfTiles * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    // Spawn tiles method as per player position
    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        if(prefabIndex == -1)
        {
           go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        }
        else
        {
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        }
        
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }

    // Delete tiles after player has passed
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);

    }

    // Randomise spawned tiles
    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while(randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
