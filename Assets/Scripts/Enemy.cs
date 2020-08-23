using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] int damageValue = 1;
    [SerializeField] float moveSpeed = 4f;

    Animator anim;
    Collider2D coll;

    private float baseMoveSpeed;
    private bool stunned = false;

    // Start is called before the first frame update
    void Start()
    {
        baseMoveSpeed = moveSpeed;
        if (anim == null) anim = GetComponent<Animator>();
        if (coll == null) coll = GetComponent<Collider2D>();
    }

    public void Stunned(float slowFactor)
    {
        moveSpeed /= slowFactor;
        stunned = true;
    }

    public void Unstunned()
    {
        moveSpeed = baseMoveSpeed;
        stunned = false;
    }

    public void DamageTaken(int dmgValue)
    {
        health -= dmgValue;
        Debug.Log(health);

        if (health <= 0)
            Death();
        else
            // play hurt sound
            return;
    }

    private void Death()
    {
        anim.SetTrigger("death");
        coll.enabled = false;
        // play death sound
        // play death particles (blood)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !stunned)
        {
            anim.SetTrigger("attack");
            // play attack sound
            // stop movement 
            collision.GetComponent<Player>().DamageTaken(damageValue);
        }
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
