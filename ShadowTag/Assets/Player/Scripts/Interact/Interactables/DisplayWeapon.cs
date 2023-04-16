using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayWeapon : Interactable
{
    [Header("Current Gun Type")]
    [SerializeField] GunManager.gunType currentGun;
    [SerializeField] AudioTrigger audioTrigger;
    private void UnlockWeapon() => GunManager.Instance.FindObjectToUnlock(currentGun);
    protected override void Interact() 
    {
        UnlockWeapon();
        audioTrigger.PlayStoryAudio();
        Destroy(gameObject); 
    }
}
