using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] tiles;

    public int spawnTimer;
    public int spawnMin;
    public int spawnMax;
    public int maxTiles;

    public GameObjectRuntimeSet floatingTileRuntimeSet;

    public GridTiles gridTiles;

    private void Awake()
    {
        RandomizeSpawnTime();
    }

    private void Start()
    {
        Tick();
    }

    public void Tick()
    {
        if (floatingTileRuntimeSet.Items.Count <= maxTiles)
        {
            spawnTimer -= 1;
        }

        if(floatingTileRuntimeSet.Items.Count <= 0)
        {
            spawnTimer = 0;
        }

        if(spawnTimer == 0 && floatingTileRuntimeSet.Items.Count <= maxTiles)
        {
            SpawnObject();
            RandomizeSpawnTime();
        }
    }

    private void SpawnObject()
    {
        int spawnPos = (int) Mathf.Floor(Random.Range(0, gridTiles.tileList.Keys.Count));
        int randomTile = (int) Mathf.Floor(Random.Range(0, tiles.Length));
        Instantiate(tiles[randomTile], new Vector3(spawnPos, 0, 0), Quaternion.identity);
    }

    private void RandomizeSpawnTime()
    {
        spawnTimer = (int)Mathf.Floor(Random.Range(spawnMin, spawnMax));
    }
}
