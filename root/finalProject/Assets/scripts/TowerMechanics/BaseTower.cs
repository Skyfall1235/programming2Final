using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    //the base clas has a base damage value, and a dmaage type.
    [SerializeField] protected int baseDamage;
    [SerializeField] protected DamageType damageType;
    [SerializeField] protected float fireRate;
    [SerializeField] protected Transform muzzle;
    [SerializeField] protected GameObject targetEnemy;
    [SerializeField] protected float maxDistance;
    [SerializeField] protected Transform enemyContainer;
    [SerializeField] protected bool canShootEnemy;

    private void Setup()
    {

    }
    protected IEnumerator ShootCoroutine(GameObject projectilePrefab)
    {
        while (canShootEnemy)
        {
            if (targetEnemy == null)
            {
                yield return null;
                continue;
            }
            //create the prefab at the mizzle location at its direction
            GameObject projectileGO = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
            //handle shooting later

            yield return new WaitForSeconds(1f / fireRate);
        }
    }

    protected void AimAtFirstEnemyInRange()
    {
        List<GameObject> enemyList = new List<GameObject>();
        GameObject[] childObjects = enemyContainer.GetComponentsInChildren<GameObject>();
        foreach (GameObject child in childObjects)
        {
            enemyList.Add(child);
        }
        foreach (GameObject enemy in enemyList)
        {
            float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
            if (distanceToEnemy <= maxDistance)
            {
                targetEnemy = enemy;
                break;
            }
        }
    }
    //it also has a fire rate and a method that uses a coroutine to shoot projectiles at the FIRST 

    //it also has an ability that is an override
    public virtual void TowerAbility()
    {
        //does nothing right now
    }
}
