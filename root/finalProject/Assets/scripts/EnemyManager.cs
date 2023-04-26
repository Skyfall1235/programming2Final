using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    private float timeBetweenAbilities;
    private float timeBetweenSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //this script will andle the order of othe enmyies to ensure they spawn in sync,
    //their special abilities occur, and for the turret to reference the order of enemys
}
