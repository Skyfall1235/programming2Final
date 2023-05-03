using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingTower : BaseTower
{
    // Start is called before the first frame update
    void Start()
    {
        damageType = DamageType.Piercing;
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        TargetFirstEnemyInRange();
        UpdateThisToo();
    }

    public override void TowerAbility()
    {
        StartCoroutine(HardShot());
    }
    IEnumerator HardShot()
    {
        Debug.Log("doubled damage for 1 second");
        baseDamage = baseDamage * 2;
        yield return new WaitForSeconds(1);
        baseDamage = baseDamage / 2;
    }
}
