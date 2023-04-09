using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Interactable
{
    [Header("Stats")]
    [SerializeField] int health;
    [SerializeField] int maxHealth;
    [SerializeField] float deathTime;
    [SerializeField] Animator animator;
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
            StartCoroutine(DieCorutine());
        }
    }
    private IEnumerator DieCorutine()
    {
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(deathTime);
        animator.ResetTrigger("Dead");
        health = maxHealth;
    }
    private void TriggerAudio(AudioClip clip) => source.PlayOneShot(clip);
}
