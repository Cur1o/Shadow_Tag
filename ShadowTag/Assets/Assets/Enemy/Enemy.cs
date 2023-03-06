using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable
{
    private PlayerUI playerUI;
    public int health;
    public int points = 50;
    // Start is called before the first frame update

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
        PlayerUI.Instance.UpdateScore(points);
    }

}
