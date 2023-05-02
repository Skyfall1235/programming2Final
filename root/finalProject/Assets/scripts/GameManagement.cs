using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [SerializeField] public PlayerData playerData;
    private int enemiesleft;
    [SerializeField] private GameObject enemyContainer;
    [SerializeField] private GameObject startWaveButton;

    //determine the wave, and start it
    private int currentWave;
    private void Start()
    {
        currentWave = playerData.waveCount;



    }

    private void Update()
    {
        //only display the start wave button when the amount of enemies on screen is 0
        if (ReadyForNextWave())
        {
            //display the button to start the next wave
            startWaveButton.SetActive(true);
        }
        //check for when the player presses the start wave button
    }

    public void onClickStartWave()
    {
        //call the start wave method on the enemymanager on the enemycontainer
        EnemyManager manager = enemyContainer.GetComponent<EnemyManager>();
        //the manager has a link to the data so it can handle itself
        //Debug.Log("clicked the start wave button");
        manager.StartWave();
        startWaveButton.SetActive(false);

    }

    private bool ReadyForNextWave() //just determines the enemy count on screen
    {
        List<GameObject> enemyList = new List<GameObject>();
        Transform enemyContainerTransform = enemyContainer.transform;

        for (int i = 0; i < enemyContainerTransform.childCount; i++)
        {
            Transform childTransform = enemyContainerTransform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            enemyList.Add(childObject);
            //Debug.Log("iterating the list to confirm no enemies in scene");
        }
        
        if (enemyList.Count > 0)
        {
            return false;
        }
        else
        {
            //Debug.Log("ready for next wave!");
            return true;
        }
    }
}
