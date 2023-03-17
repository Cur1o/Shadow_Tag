using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayWeapon : Interactable
{
    [Header("Current Gun Type")]
    [SerializeField] GunManager.gunType currentGun;
    private void UnlockWeapon() => GunManager.Instance.FindObjectToUnlock(currentGun);
    protected override void Interact() 
    {
        Debug.Log("Interacted width : " + gameObject.name);
        UnlockWeapon(); 
        Debug.Log("Unlocked Weapon : " + gameObject.name); 
    }
}
