using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunManager.gunType gunVariant;
    public bool weaponUnlocked;
    public bool weaponActive;
    public Material Neon;
    private Color currentColor;
    //Weapon specs
    public int damage;
    public int ammunition;
    public int ammunitionMax;
    public float range;
    public float realoadTime;
    public float shootCooldown;
    [Header("Audio Clips")]
    public AudioClip reloading;
    public AudioClip shooting;
    public AudioClip empty;

    private void Start() => ChangeColor();
    
    public void ChangeColor()
    {
        float transitionValue = (float)ammunition / ammunitionMax;
        currentColor =new Color((int)(255 * (1 - transitionValue*2)), (int)(255 * transitionValue), 0);
        Neon.SetColor("_EmissionColor", currentColor);
    }
}

