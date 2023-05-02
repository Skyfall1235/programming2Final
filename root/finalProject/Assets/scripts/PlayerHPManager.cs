using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPManager : MonoBehaviour
{
    //this script just updates the player data whith the current HP
    [SerializeField] private PlayerData playerData;
    [SerializeField] private int HP;

    private void Start()
    {
        HP = playerData.health;
    }

    private void OnTriggerEnter(Collider other)
    {
        //detect when other objects ocollide with me and then kill them, taking thier remaining HP as damage
        if (other.gameObject.tag == "enemy")
        {
            Enemy enemyBase = other.GetComponent<Enemy>();
            if (enemyBase != null)
            {
                HP -= enemyBase.Health;
            }
            else
            {
                Debug.Log("enemy is missing its script");
            }
            
        }
        //do we need anything here?
    }


}
