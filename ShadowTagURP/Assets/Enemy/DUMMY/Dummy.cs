using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Interactable
{
    [Header("Stats")]
    [SerializeField] int health;
    [SerializeField] int maxHealth;
    [SerializeField] float deathTime;
    [SerializeField] GameObject pivot;
    [Header("Audio")]
    [SerializeField] bool hasAudio;
    [SerializeField] AudioClip triggerAudio;
    [SerializeField] AudioClip hitAudio;
    [SerializeField] AudioClip dieAudio;
    AudioSource source;
    // Start is called before the first frame update
    private void Start()
    {
        if (hasAudio)
        {
            source = GetComponent<AudioSource>();
            TriggerAudio(triggerAudio);
        }
    }
    public void Hit(int damage)
    {
        if (hasAudio) TriggerAudio(hitAudio);
        health -= damage;
        if (health <= 0)
        {
            if (hasAudio) TriggerAudio(dieAudio);
            Die();
        }
    }
    private void Die() => StartCoroutine(DieCorutine());
    private IEnumerator DieCorutine()
    {
        pivot.transform.rotation = Quaternion.Euler(-90f,0,0);
        yield return new WaitForSeconds(deathTime);
        pivot.transform.rotation = Quaternion.Euler(0, 0, 0);
        health = maxHealth;
    }
    private void TriggerAudio(AudioClip clip) => source.PlayOneShot(clip);
}
