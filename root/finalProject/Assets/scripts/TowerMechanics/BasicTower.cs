using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : BaseTower
{
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        TargetFirstEnemyInRange();
    }
}
