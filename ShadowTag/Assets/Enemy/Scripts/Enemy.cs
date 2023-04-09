using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable
{
    private enum variantSlime {lightBlue = 0, blue = 1, red = 2, green = 3, gold =4 };
    [Header("Stats")]
    [SerializeField] int health;
    [SerializeField] int points = 50;
    [SerializeField] float lifeTime = 3f;
    [Header("Slime Variant")]
    [SerializeField] private bool isSlime;
    [SerializeField] private bool isRandom;
    [SerializeField] private variantSlime variant;
    [SerializeField] private List<Texture2D> textures;
    private Material material;
    [Header("Audio")]
    [SerializeField] bool hasAudio;
    [SerializeField] AudioClip triggerAudio;
    [SerializeField] AudioClip hitAudio;
    [SerializeField] AudioClip dieAudio;
    AudioSource source;
    // Start is called before the first frame update
    private void Start()
    {
        if (isSlime) 
        {
            if(isRandom) RandomSlime();
            SetSlimeTextures();
            if ((int)variant == 2) points *= 2;
            else if ((int)variant == 3) points *= 4;
            else if ((int)variant == 4) points *= 8;
        }
        if (hasAudio)
        {
            source = GetComponent<AudioSource>();
            TriggerAudio(triggerAudio);
        }
        StartCoroutine(Live());
    }
    private void RandomSlime()
    {
        var randomSlime = Mathf.Round(Random.Range(0, textures.Count));
        variant = (variantSlime)randomSlime;
    }
    private void SetSlimeTextures()
    {
        material = GetComponent<Renderer>().materials[1];
        material.color = Color.white;
        material.SetTexture("_MainTex", textures[(int)variant]);
        material.SetTexture("_EmissionMap", textures[(int)variant]);
        material.SetColor("_EmissionColor", Color.white);
        material.EnableKeyword("_EMISSION");
    }    
    public void Hit(int damage)
    {
        if (hasAudio)TriggerAudio(hitAudio);
        health -= damage;
        if (health <= 0)
        {
            AddPoints();
            if (hasAudio) TriggerAudio(dieAudio);
            Die();
        }   
    }
    private IEnumerator Live()
    {
        yield return new WaitForSeconds(lifeTime);
        Die();
    }
    private void Die() => Destroy(gameObject);
    private void AddPoints() => PlayerUI.Instance.UpdateScore(points);
    private void TriggerAudio(AudioClip clip) => source.PlayOneShot(clip);
}
