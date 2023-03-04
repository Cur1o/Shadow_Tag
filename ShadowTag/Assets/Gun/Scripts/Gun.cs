using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunManager.gunType gunVariant;
    public bool weaponUnlocked;
    public bool weaponActive;
    //Weapon specs
    [SerializeField]float damage;
    public float range;
    public float amonition;
    public float amonitionMax;
    public float realoadTime;
    public float shootCooldown;



}

