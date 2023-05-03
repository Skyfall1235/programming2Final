using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public DamageType damageType;
    int speed = 5;

    private void Start()
    {
        Destroy(gameObject, 3);
    }
    //go forward from the muzzle
    private void Update()
    {
        Vector3 bulletVelocity = transform.forward * speed;
        transform.position += bulletVelocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject);
        Enemy enemyScript = other.gameObject.GetComponent<Enemy>();
        Debug.Log(enemyScript);
        enemyScript.DetermineTrueDamageValue(damage, damageType);
        Destroy(gameObject);
    }
}
