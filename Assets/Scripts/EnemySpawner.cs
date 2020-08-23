using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnMinDelay = 2f;
    [SerializeField] float spawnMaxDelay = 4f;

    [SerializeField] List<GameObject> enemies;
    [SerializeField] List<Transform> targets;
    [SerializeField] List<Transform> spawnPoints;

    private float spawnTimer = 0f;
    private float spawnTime;
    private bool spawnReady = true;

    // Start is called before the first frame update
    void Start()
    {
        Randomize();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer <= spawnTime)
            spawnTimer += Time.deltaTime;
        else if (spawnTimer >= spawnTime && spawnReady)
        {
            spawnTimer = 0f;
            Instantiate(enemies[Random.Range(0, enemies.Count)], spawnPoints[Random.Range(0, spawnPoints.Count)]);
            Randomize();
        }
    }

    private void Randomize()
    {
        spawnTime = Random.Range(spawnMinDelay, spawnMaxDelay);
    }

    public void SetSpawnDelay(float minDelay, float maxDelay)
    {
        spawnMinDelay -= minDelay;
        spawnMaxDelay -= maxDelay;
    }

    public void StopEnemySpawn()
    {
        spawnReady = false;
    }
}
