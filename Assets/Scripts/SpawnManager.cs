using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] tiles;

    public IntVariable activeFloatingTiles;

    public int spawnTimer;
    public int spawnMin;
    public int spawnMax;

    private void Awake()
    {
        RandomizeSpawnTime();
    }

    private void OnDisable()
    {
        activeFloatingTiles.SetValue(0);
    }

    public void Tick()
    {
        Debug.Log("Tick");
        spawnTimer -= 1;

        if(activeFloatingTiles.Value == 0)
        {
            spawnTimer = 0;
        }

        if(spawnTimer == 0)
        {
            SpawnObject();
            RandomizeSpawnTime();
        }
    }

    private void SpawnObject()
    {
        int spawnPos = (int) Mathf.Floor(Random.Range(0, 9));
        int randomTile = (int) Mathf.Floor(Random.Range(0, tiles.Length));
        Instantiate(tiles[randomTile], new Vector3(spawnPos, 0, 0), Quaternion.identity);
    }

    private void RandomizeSpawnTime()
    {
        spawnTimer = (int)Mathf.Floor(Random.Range(spawnMin, spawnMax));
    }
}
