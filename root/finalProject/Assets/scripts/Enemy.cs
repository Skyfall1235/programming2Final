using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    //enemies need a relevant damage type to the player, an HP t chip away, a damage to players health, and some ability to call special abilites?
    protected int enemyHP;
    protected int damageToPlayer;
    protected DamageType damageType;




    //public DealDamageToPlayer()


    public virtual void SpecialAbility(DamageType type)
    {
        Debug.Log("no special ability!");
    }
}


