using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayWeapon : Interactable
{
    [SerializeField] GameObject prefab;
    Gun script;
    GunManager.gunType currentGun;

    private void Start()
    {
        script = prefab.GetComponent<Gun>();
        currentGun = script.gunVariant;
    }
    private void UnlockWeapon() => GunManager.Instance.FindObjectToUnlock(script);
    
    protected override void Interact() => Debug.Log("Unlocked Weapon : " + gameObject.name);


}
