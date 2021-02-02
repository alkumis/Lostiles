using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] tiles;

    public int spawnTimer;
    public int spawnMin;
    public int spawnMax;

    public GameObjectRuntimeSet floatingTileRuntimeSet;

    private void Awake()
    {
        RandomizeSpawnTime();
    }

    public void Tick()
    {
        spawnTimer -= 1;

        if(floatingTileRuntimeSet.Items.Count <= 0)
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
