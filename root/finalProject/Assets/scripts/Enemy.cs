using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    //enemies need a relevant damage type to the player, an HP t chip away, a damage to players health, and some ability to call special abilites?
    protected int enemyHP;
    protected int enemyMaxHP;
    protected int enemyArmor;
    protected int damageToPlayer;
    protected EnemyStyle style;
    protected int scoreValue;





    //dmaage dealt to the player can be handled by the health script, just grab the damage type and player.

    //damage to the enemy can be resaolved here using polymorphism
    //it needs to compare the incoming damage type to the type of enemy
    public void DetermineTrueDamageValue(int baseDamage, DamageType type)
    {
        switch (style)
        {
            case EnemyStyle.Armored:
                switch (type)
                {
                    case DamageType.Piercing:
                        //take double damage
                        break;
                    case DamageType.Explosive:
                        // run the equivilent of the base damage, but remove my armor value
                        break;

                    default:
                        // do nothing
                        break;
                }
                break;

            case EnemyStyle.HeatShielded:
                switch (type)
                {
                    case DamageType.Heat:
                        // take normal damage
                        break;

                    default:
                        // do nothing except maybe show a -0 hp lol
                        break;
                }
                break;

            case EnemyStyle.Healing:
                // probably have a percent damage reduction? or nothing at all
                break;

            default: //runs the EnemyStyle.Base stuff
                // //runs the base damage calculation
                break;
        }
    }

    public void SetUpValues()
    {

    }

    private void StripArmor()
    {
        //i want this as a method just so i can read my code in english not in numbers
        enemyArmor = 0;
    }

    private void TakeTrueDamage(int damage)
    {
        enemyHP -= damage;
        if (enemyHP <= 0)
        {
            Death();
        }
    }

    protected void Death()
    {

    }

    //for subclases, when this is called (every so often, like 5~ seconds?) it will perform 1 action based on the tpye of the enemy

    public virtual void SpecialAbility()
    {
        Debug.Log("no special ability!");
    }
}


