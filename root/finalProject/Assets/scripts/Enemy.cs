using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //enemies need a relevant damage type to the player, an HP to chip away, a damage to players health, and some ability to call special abilites?
    //gonna be expirienmenting with protected varaibles, see how they work ig?
    protected int enemyHP;
    public int Health
        { get { return enemyHP; } } 
    protected int enemyMaxHP;
    protected int enemyArmor;
    protected EnemyStyle style;
    protected int scoreValue;
    [SerializeField] protected string endTrigger;
    [SerializeField] protected float speed;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected GameObject target;

    private void Start()
    {
        StartUp();
    }
    private void Update()
    {
        BaseClassUpdates();
    }
    private void StartUp()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find(endTrigger);
    }
    private void BaseClassUpdates()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
            agent.speed = speed;
        }
    }
    //damage dealt to the player can be handled by the health script, just grab the damage type and player.
    //damage to the enemy can be resolved here using polymorphism
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
                        TakeTrueDamage(baseDamage, 2);
                        break;
                    case DamageType.Explosive:
                        // run the equivilent of the base damage, but remove my armor value
                        TakeTrueDamage(baseDamage, 1);
                        StripArmor();
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
                        TakeTrueDamage(baseDamage, 1);
                        break;

                    default:
                        // do nothing except maybe show a -0 hp lol
                        break;
                }
                break;

            case EnemyStyle.Healing:
                // probably have a percent damage reduction? or nothing at all
                TakeTrueDamage(baseDamage, 1);
                break;

            default: //runs the EnemyStyle.Base stuff
                // //runs the base damage calculation
                break;
        }
    }

    public void SetUpValues()
    {
        enemyHP = enemyMaxHP;
        scoreValue = enemyMaxHP;
    }

    private void StripArmor()
    {
        //i want this as a method just so i can read my code in english not in numbers
        enemyArmor = 0;
    }

    private void TakeTrueDamage(int damage, int multiplier)
    {
        //reduces the damage based no the armor value. anything that makes the raw damage go negative gets nulled to 0
        int rawDamage = damage * multiplier;
        int valueAfterArmorReduction = rawDamage - enemyArmor;
        if (valueAfterArmorReduction < 0)
        {
            valueAfterArmorReduction = 0;
        }
        //any dmaage thats gets through or doesnt have a reductino just gos right in
        enemyHP -= valueAfterArmorReduction;
        if (enemyHP <= 0)
        {
            Death();
        }
    }
    //kill the enemy, give the player the score, and provide some funds?
    private void Death()
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        GameManagement managerScript = gameManager.GetComponent<GameManagement>();
        if (gameManager != null)
        {
            managerScript.playerData.score += scoreValue;
            managerScript.playerData.moneyAmount += scoreValue;
        }
        //the last thing, after all info is updated
        Destroy(gameObject);
    }

    //for subclases, when this is called (every so often, like 5~ seconds?) it will perform 1 action based on the tpye of the enemy

    public virtual void SpecialAbility()
    {
        Debug.Log("no special ability!");
        //in overrides, this will start the relevant coroutine
    }
}


