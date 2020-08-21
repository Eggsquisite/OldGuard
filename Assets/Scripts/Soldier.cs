using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] Sword sword = null;
    [SerializeField] float attackMaxWait = 2f;
    
    Animator anim;
    private bool attackReady = true;
    private float attackTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && attackReady)
        {
            anim.SetTrigger("attack");
            attackReady = false;
        }

        if (!attackReady)
            AttackTimer();
    }

    private void AttackTimer()
    {
        if (attackTimer <= attackMaxWait)
        {
            attackTimer += Time.deltaTime;
        }
        else if (attackTimer >= attackMaxWait)
        {
            attackReady = true;
            attackTimer = 0;
        }
    }

    public void AttackSet(float status)
    {
        if (status == 0)
            sword.Attacking(false);
        else if (status == 1)
            sword.Attacking(true);
    }
}
