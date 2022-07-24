using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMoreEnemyOnDeath : MonoBehaviour {
    public GameObject spawnEnemyPrefab;
    public int spawnEnemyCount;

    void OnDestroy() {
        Debug.Log("triggered death");
        for (int i = 0; i < spawnEnemyCount; i++)
            Spawn(spawnEnemyPrefab, GetComponent<PathBehavior>().pathName);
    }

    private void Spawn(GameObject enemyPrefab,string pathName) {
        GameObject enemy = Instantiate(enemyPrefab, transform);
        enemy.GetComponent<PathBehavior>().pathName = pathName;
    }
}