using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EnemyManager : MonoBehaviour
{

    //all possible enemy types, with thier weights and startWaves
    public List<EnemyDataStruct> enemyData = new List<EnemyDataStruct>();
    private int currentWave;
    private float timeBetweenAbilities;
    private float timeBetweenSpawn;
    [SerializeField] private GameObject spawnPoint;

    //how long the couroutine should wait
    public float enemySpawnDelay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //this should be called before EVERY WAVE (yes i know thats expensive but i dont want to preload levels)
    private List<GameObject> DetermineEnemiesToSpawn()
    {
        List<GameObject> enemiesToSpawn = new List<GameObject>();
        List<EnemyDataStruct> currentSpawnableChoices = new List<EnemyDataStruct>();

        int waveSpawnWeightTotal = currentWave * 2;


        //first, find all spawnable choices, THEN start adding the items to the list bases on 
        foreach (EnemyDataStruct enemyStruct in enemyData)
        {
            if (enemyStruct.startWave >= currentWave)
            {
                //the structs that were found to be availble for the wave
                currentSpawnableChoices.Add(enemyStruct);
            }
        }    
        while (waveSpawnWeightTotal > 0)
        {
            //in case of error or fuck up
            // if their aint no objects in the list, the method failed. figure it out 4head
            if (currentSpawnableChoices.Count == 0)
            {
                Debug.Log("no spawnable objects!");
                break;
            }

            //if we have available weight, check and see if what we can grab another randomised object that the method can afford.
            //keeps iterating til it gwts down to 1, where it will take the normal enemy prefab, and close.
            int chosenItem = Random.Range(0, currentSpawnableChoices.Count);
            if ((waveSpawnWeightTotal - currentSpawnableChoices[chosenItem].enemyValue) >= 0)
            {
                waveSpawnWeightTotal -= currentSpawnableChoices[chosenItem].enemyValue;
                enemiesToSpawn.Add(currentSpawnableChoices[chosenItem].enemyPrefab);
            }
        }
        //closes and returns

        return enemiesToSpawn;
    }


    //to start the next wav of enemies,

    // spawns enemies 1 by 1 using the delay we set earlier
    private IEnumerator SpawnEnemies(List<GameObject> enemies)
    {
        foreach (GameObject enemyPrefab in enemies)
        {
            //how do i spawn at a start position again?
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(enemySpawnDelay);
        }
    }

    public void StartWave()
    {
        StartCoroutine(SpawnEnemies(DetermineEnemiesToSpawn()));
        currentWave++;
        //work on a way to use the playerData WaveCount instead of controlling it privately
    }

    private void ActivateSpecialAbilities()
    {

    }


    //this script will andle the order of othe enmyies to ensure they spawn in sync,
    //their special abilities occur, and for the turret to reference the order of enemys





    [System.Serializable]
    public struct EnemyDataStruct
    {
        //is just for organisation
        [SerializeField] private string enemyName;
        //the prefab itself
        public GameObject enemyPrefab;
        //the first wave it can appear on
        public int startWave;
        //likelyhood of spawning
        public int enemyValue;
    }
}
