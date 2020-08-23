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

    public void DamageTaken(int dmgVal)
    {
        health -= dmgVal;

        if (health <= 0)
            Death();
        else
            // play hurt sound
            return;
    }

    private void Death()
    { 
        // game over text

        // restart level
    }


}
