using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager
{
    public HashSet<GameObject> mEnemies = new HashSet<GameObject>();
    private int enemiesDefeated = 0;

    public void AddEnemy(GameObject enemy)
    {
        mEnemies.Add(enemy);
    }
    public void RemoveEnemy(GameObject enemy)
    {
        if (mEnemies.Remove(enemy))
        {
            enemiesDefeated++;
        }
    }
    public int GetEnemiesDefeated()
    {
        return enemiesDefeated;
    }
    public void RemoveAllEnemies()
    {
        mEnemies.Clear();
    }
    public bool IsEmpty() {
        return mEnemies.Count == 0;
    }
    public GameObject GetClosestEnemy(Vector3 position)
    {
        GameObject closest = null;
        float closestDistance = float.MaxValue;
        foreach (GameObject enemy in mEnemies)
        {
            float distance = (enemy.transform.position - position).magnitude;
            if (distance < closestDistance)
            {
                closest = enemy;
                closestDistance = distance;
            }
        }
        return closest;
    }
    public List<GameObject> GetEnemiesInRadius(Vector2 center, float radius)
    {
        List<GameObject> enemiesInRadius = new List<GameObject>();
        foreach (GameObject enemy in mEnemies)
        {
            Vector2 enemyPos = enemy.transform.position;
            float distance = (enemyPos - center).magnitude;
            if (distance < radius)
            {
                enemiesInRadius.Add(enemy);
            }
        }
        return enemiesInRadius;
    }
}