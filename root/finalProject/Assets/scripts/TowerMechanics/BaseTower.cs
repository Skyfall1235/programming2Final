using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseTower : MonoBehaviour
{
    //the base clas has a base damage value, and a dmaage type.
    [SerializeField] protected int baseDamage = 1;
    [SerializeField] protected DamageType damageType = DamageType.Normal;
    [SerializeField] protected float fireRate = 2;
    [SerializeField] protected Transform muzzle;
    [SerializeField] protected GameObject targetEnemy;
    [SerializeField] protected float maxDistance = 7;
    [SerializeField] protected GameObject enemyContainer;
    [SerializeField] protected bool canShootEnemy;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected GameObject turret;

    public void Setup()
    {
        enemyContainer = GameObject.Find("EnemyContainer");
        InvokeRepeating("TowerAbility", 1, 10f);
        StartCoroutine(ShootCoroutine());
    }
    public void UpdateThisToo()
    {
        if (targetEnemy != null)
        {
            turret.transform.LookAt(targetEnemy.transform);
        }
    }
    protected IEnumerator ShootCoroutine()
    {
        while (canShootEnemy)
        {
            if (targetEnemy == null)
            {
                yield return null;
                continue;
            }
            //create the prefab at the mizzle location at its direction
            GameObject projectile = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
            Vector3 targetDirection = targetEnemy.transform.position - transform.position;
            projectile.transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            projectile.GetComponent<Projectile>().damage = baseDamage;
            projectile.GetComponent<Projectile>().damageType = damageType;
            //handle shooting later

            yield return new WaitForSeconds(1f / fireRate);
        }
    }

    protected void TargetFirstEnemyInRange()//locates the best enemy. targets the first enemy in the list that is within range
    {
        List<GameObject> enemyList = new List<GameObject>();
        for (int i = 0; i < enemyContainer.transform.childCount; i++)
        {
            enemyList.Add(enemyContainer.transform.GetChild(i).gameObject);
        }
        foreach (GameObject enemy in enemyList)
        {
            float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
            if (distanceToEnemy <= maxDistance)
            {
                targetEnemy = enemy;
                break;
            }
            else
            {
                targetEnemy = null; 
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
