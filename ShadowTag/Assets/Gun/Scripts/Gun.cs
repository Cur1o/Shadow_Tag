using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Gun : MonoBehaviour
{
    public GunManager.gunType gunVariant;
    public bool weaponUnlocked;
    public bool weaponActive;
    public Material neon;
    private Color currentColor;
    [Header("Weapon Specs")]
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
    [Header("Components")]
    public VisualEffect effect;
    public AudioSource audioSource;
    public Animator animator;
    private void Start() => ChangeColor();
    public void ChangeColor()
    {
        float transitionValue = (float)ammunition / ammunitionMax;
        currentColor =new Color((int)(255 * (1 - transitionValue*2)), (int)(255 * transitionValue), 0);
        neon.SetColor("_EmissionColor", currentColor);
    }
}

