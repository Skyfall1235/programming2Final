using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManagement : MonoBehaviour
{
    public GameObject givenTower;
    public GameObject towerSpawnLocation;




}
public enum EnemyStyle
{ 
    Base, //normal, everything does normal damage to them
    Armored, //can only be damaged by piercing or explosive
    HeatShielded, //can only be damaged with heat
    Healing, // have large HP pools and can regenerate

}
public enum DamageType
{ 
    Normal, //normal dmaage
    Piercing, //double damage to normal targets or normal damage to armored
    Explosive, // breaks armor completely
    Heat, // x1.5 damage to base, or normal to heatshielded


}



