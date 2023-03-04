using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable
{
    private PlayerUI playerUI;
    public int health;
    public int points;
    // Start is called before the first frame update
    void Start()
    {
        playerUI = PlayerUI.Instance;
    }

    public void Hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        AddPoints();
        Destroy(gameObject);
    }
    private void AddPoints()
    {
        playerUI.UpdateScore(points);
    }

}
