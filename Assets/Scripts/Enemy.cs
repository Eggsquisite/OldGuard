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

    // Start is called before the first frame update
    void Start()
    {
        baseMoveSpeed = moveSpeed;
        if (anim == null) anim = GetComponent<Animator>();
        if (coll == null) coll = GetComponent<Collider2D>();
    }

    public void Slowed(float slowFactor)
    {
        moveSpeed /= slowFactor;
    }

    public void Unslowed()
    {
        moveSpeed = baseMoveSpeed;
    }

    public void Damage(int dmgValue)
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

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // anim.SetTrigger("attack");
            // play attack sound
            // stop movement 
            collision.GetComponent<Player>().Damage(damageValue);
        }
    }
}
