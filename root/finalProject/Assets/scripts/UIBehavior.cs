using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBehavior : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private GameObject purchaseMenu;
    [SerializeField] private GameObject statusMenu;

    [SerializeField] private TextMeshProUGUI HPValue;
    [SerializeField] private TextMeshProUGUI waveValue;
    [SerializeField] private TextMeshProUGUI playerCurrency;


    private void Start()
    {

    }

    private void Update()
    {
        UpdateStatusUI();
    }



    GameObject selectedTowerBase = null;


    #region Modification of Towers

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

        //Display the post process glow
    }
    //places the tower if the player can afford it. data is then saved to the towermanagement script
    public void PlaceTower(GameObject selectedTowerBase, GameObject selectedTower, int price)
    {
        if (CheckIfTowerExists(selectedTowerBase) && CheckIfPlayerCanAffordTower(price))
        {
            //instatiate the selected prefab here, and subtract the price, and give the towerbase component the selected gameobject
            GameObject spawnedObject = Instantiate(selectedTower, selectedTowerBase.GetComponent<TowerManagement>().towerSpawnLocation.transform.position, Quaternion.identity);
            selectedTowerBase.GetComponent<TowerManagement>().givenTower = spawnedObject;
            
            //selectedTowerBase
            playerData.moneyAmount -= price;
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
        //check the tower base for a link to a placed tower. if there is no tower, return false
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



    bool menuIsOpen = true;
    public void OpenPurchaseMenu()
    {
        menuIsOpen = !menuIsOpen;

        if(menuIsOpen)
        {
            //display he purchase panel
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
