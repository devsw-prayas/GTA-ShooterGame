using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Vector3 worldBounds;
    public float spawnDelay;
    public GameObject enemyObject;
    public Transform enemyParent;

    float lastSpawnTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lastSpawnTime += Time.deltaTime;

        if (lastSpawnTime >= spawnDelay && enemyParent.childCount <= 10) {
            spawnEnemy();
        }
    }

    void spawnEnemy() {
        float rndx = Random.Range(-worldBounds.x, worldBounds.x);
        float rndz = Random.Range(-worldBounds.z, worldBounds.z);
        Physics.Raycast(new Vector3(rndx, 30, rndz), new Vector3(0, -1, 0), out RaycastHit hitInfo, 32);
        Instantiate(enemyObject, hitInfo.point + new Vector3(0, 3, 0), Quaternion.identity, enemyParent);
        lastSpawnTime = 0;
    }
}
