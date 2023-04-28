using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class UIBehavior : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private GameObject purchaseMenu;
    [SerializeField] private GameObject statusMenu;

    [SerializeField] private TextMeshProUGUI HPValue;
    [SerializeField] private TextMeshProUGUI waveValue;
    [SerializeField] private TextMeshProUGUI playerCurrency;

    [SerializeField] public List <TowerInfo> towerPrefabInfo = new List<TowerInfo> ();
    

    private void Start()
    {

    }

    private void Update()
    {
        UpdateStatusUI();
    }

    #region Modification of Towers
    GameObject selectedTowerBase = null;

    public void SelectTowerBase()
    {
        //Get the GameObject that was clicked
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("tower"))
        {
            //Save the selected GameObject
            selectedTowerBase = hit.collider.gameObject;
            Debug.Log("Selected tower: " + selectedTowerBase.name);
        }
        else
        {
            //if the player clicks off, null the selection
            selectedTowerBase = null;
        }

        //Display the post process glow
        //should we also display the selection menu?
    }
    //places the tower if the player can afford it. data is then saved to the towermanagement script

    //how do we call this method?

    //search the list using the int to find the right one. i cant seem to let a method take a struct as a parameter :(
    public void PlaceTower(int selectedTower)
    {
        TowerInfo towerInfo = towerPrefabInfo[selectedTower];
        //if the base doesnt have a tower and the player can afford it, place the tower.
        if (!CheckIfTowerExists(selectedTowerBase) && CheckIfPlayerCanAffordTower(towerInfo.towerPrice))
        {
            //instatiate the selected prefab here, and subtract the price, and give the towerbase component the selected gameobject
            GameObject spawnedObject = Instantiate(towerInfo.towerPrefab, selectedTowerBase.GetComponent<TowerManagement>().towerSpawnLocation.transform.position, Quaternion.identity);
            selectedTowerBase.GetComponent<TowerManagement>().givenTower = spawnedObject;
            
            //selectedTowerBase
            playerData.moneyAmount -= towerInfo.towerPrice;
            //nullify the selected base so we can interct with something else
            selectedTowerBase = null;
        }
        else if (CheckIfPlayerCanAffordTower(towerInfo.towerPrice))
        {
            //tell the player they cannot afford it
        }
        else if (!CheckIfTowerExists(selectedTowerBase))
        {
            //there is a tower already there
        }
        else
        {
            //no tower selected
        }

    }
    public void DestroyTower(GameObject selectedTowerBase)
    {
        //if there is a tower exists on the towerbase, destroy the TOWER PREFAB
        if(CheckIfTowerExists(selectedTowerBase))
        {
            //destroys the tower associated with the tower base
            Destroy(selectedTowerBase.GetComponent<TowerManagement>().givenTower);
        }
    }


    private bool CheckIfTowerExists(GameObject selectedTowerBase)
    {
        //check the tower base for a link to a placed tower. if there is no tower built, return false
        if(selectedTowerBase.GetComponent<TowerManagement>().givenTower != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CheckIfPlayerCanAffordTower(int price)
    {
        if (playerData.moneyAmount - price < 0)
        { return false; }
        else { return true; }
    }

    #endregion

    #region Ui status Updating

    private void UpdateStatusUI()
    {
        HPValue.text = $"HP : {playerData.health.ToString()}";
        waveValue.text = $"Wave : {playerData.waveCount.ToString()}";
        playerCurrency.text = $"Credits : {playerData.moneyAmount.ToString()}";
    }



    bool menuIsOpen = false;
    public void OpenPurchaseMenu()
    {
        menuIsOpen = !menuIsOpen;

        if(menuIsOpen)
        {
            //display the purchase panel
            purchaseMenu.SetActive(false);
        }
        if(!menuIsOpen)
        {
            //hide the purchase panel
            purchaseMenu.SetActive(true);
        }
    }




    #endregion
}
[System.Serializable]
public struct TowerInfo
{
    public string towerName;
    public int towerPrice;
    public GameObject towerPrefab;
}
