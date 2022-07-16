using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager {
    private HashSet<GameObject> mEnemies;
    public EnemyManager() {
        mEnemies = new HashSet<GameObject>();
    }
    public void AddEnemy(GameObject enemy) {
        mEnemies.Add(enemy);
    }
    public void RemoveEnemy(GameObject enemy) {
        mEnemies.Remove(enemy);
    }
    public void RemoveAllEnemies() {
        mEnemies.Clear();
    }
    public GameObject GetClosestEnemy() {
        GameObject closest = null;
        float closestDistance = float.MaxValue;
        foreach (GameObject enemy in mEnemies) {
            float distance = (enemy.transform.position - GameManager.sTheGlobalBehavior.mHero.transform.position).magnitude;
            if (distance < closestDistance) {
                closest = enemy;
                closestDistance = distance;
            }
        }
        return closest;
    }
}