using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Enemy[] enemies;

	private void Start()
    {
        enemies = FindObjectsOfType<Enemy>();
        CalculateEnemyDamage();
    }

    public void CalculateEnemyDamage()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.TakeDamage += (Collision c) => 
            {
                enemy.hitpoints = enemy.hitpoints - 1;

                if(enemy.hitpoints <= 0)
                    Destroy(enemy);

                Debug.Log(enemy.hitpoints);
            };
        }
    }
}
