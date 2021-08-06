using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    private Vector3 spawnPos = new Vector3(25, 0, 0);

    private PlayerController playerControllerScript;

    private float startDelay = 2.0f;
    private float spawnDelay = 1.5f;

    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, spawnDelay);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void SpawnObstacle() {
        if(playerControllerScript.gameOver == false) {
            int prefabIndex = Random.Range(0, obstaclePrefabs.Length);
            Instantiate(obstaclePrefabs[prefabIndex], spawnPos, obstaclePrefabs[prefabIndex].transform.rotation);
        }
    }
}
