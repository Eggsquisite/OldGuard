using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] float damageValue = 1;
    [SerializeField] float attackCD = 2f;
    [SerializeField] EnemyAI enemyAI = null;

    [Header("SFX")]
    [SerializeField] AudioClip deathSFX = null;

    [Header("Pickup")]
    [Range(0.0f, 1.0f)]
    [SerializeField] float heartDropChance;
    [SerializeField] GameObject heartPickup = null;

    Animator anim;
    Collider2D coll;

    private float attackTimer = 0f;
    private float baseMoveSpeed;
    private bool stunned = false;
    private bool attackReady = true;

    // Start is called before the first frame update
    void Start()
    {
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
            if (enemyAI != null)
                enemyAI.SetUnstunned();
        }
    }

    public void Stunned(float slowFactor)
    {
        stunned = true;
        anim.SetBool("stunned", true);
        if (enemyAI != null)
            enemyAI.SetStunned(slowFactor);
    }

    public void Unstunned()
    {
        stunned = false;
        attackReady = false;
        anim.SetBool("stunned", false);

        if (enemyAI != null)
            enemyAI.SetUnstunned();
    }

    public void DamageTaken(int dmgValue, Transform player)
    {
        health -= dmgValue;
        if (enemyAI != null)
            enemyAI.SetTarget(player);

        if (health <= 0)
            Death();
        else
            // play hurt sound
            return;
    }

    private void Death()
    {
        coll.enabled = false;
        if (enemyAI != null)
            enemyAI.SetStunned(0);
        anim.SetTrigger("death");
        Camera.main.GetComponent<AudioSource>().PlayOneShot(deathSFX);
        // play death particles (blood)
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && attackReady && !stunned)
        {
            attackReady = false;
            if (enemyAI != null)
                enemyAI.SetStunned(0);
            anim.SetTrigger("attack");
            collision.GetComponent<Player>().DamageTaken(damageValue);
        }
    }
    private void Destroy()
    {
        var rand = Random.Range(0.0f, 1.0f);
        if (rand <= heartDropChance)
        {
            Debug.Log("Instantiating");
            Instantiate(heartPickup, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
