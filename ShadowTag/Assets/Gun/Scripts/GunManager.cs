using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public enum gunType { pistol = 0, rifle = 1, shotgun = 2, assaultRifle = 3 };
    
    
    [SerializeField] GameObject[] prefab;
    GameObject[] instanciatedWeapons;
    GameObject[] activeWeapons;
    Gun[] guns;
    GameObject currentActiveWeapon;
    private void Start()
    {
        InstanciateWeapons();
    }
    private void InstanciateWeapons()
    {
        foreach (var weapon in prefab)
        {
            Vector3 position = transform.position;
            var obj = Instantiate(weapon, position, Quaternion.identity);
            var script = obj.GetComponent<Gun>();
            if (script.weaponActive == true)
            {

            }
        }
    }
    public void Reload()
    {

    }
    public void Shoot()
    {

    }
    public void Aim()
    {

    }
}
