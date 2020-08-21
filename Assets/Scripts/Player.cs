using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int health = 3;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if (anim == null) anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMovementAnim(bool status)
    {
        anim.SetBool("isWalking", status);
    }

    private void Damage()
    {
        health -= 1;

        if (health <= 0)
            Death();
    }

    private void Death()
    { 
        // game over text

        // restart level
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            Damage();
    }
}
