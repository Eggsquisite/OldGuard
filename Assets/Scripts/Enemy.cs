using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] int damageValue = 1;
    [SerializeField] float attackCD = 2f;
    [SerializeField] float moveSpeed = 4f;

    Animator anim;
    Collider2D coll;

    private float attackTimer = 0f;
    private float baseMoveSpeed;
    private bool stunned = false;
    private bool attackReady = true;

    // Start is called before the first frame update
    void Start()
    {
        baseMoveSpeed = moveSpeed;
        if (anim == null) anim = GetComponent<Animator>();
        if (coll == null) coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!attackReady)
            AttackCooldown();
    }

    private void AttackCooldown()
    {
        if (attackTimer < attackCD)
            attackTimer += Time.deltaTime;
        else if (attackTimer >= attackCD)
        {
            attackTimer = 0;
            attackReady = true;
        }
    }

    public void Stunned(float slowFactor)
    {
        stunned = true;
        moveSpeed /= slowFactor;
        anim.SetBool("stunned", true);
    }

    public void Unstunned()
    {
        stunned = false;
        attackReady = false;
        moveSpeed = baseMoveSpeed;
        anim.SetBool("stunned", false);
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && attackReady && !stunned)
        {
            attackReady = false;
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
