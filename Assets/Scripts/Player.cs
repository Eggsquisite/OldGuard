using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager = null;
    [SerializeField] AudioClip hurtSFX = null;
    [SerializeField] float invulnMaxTime = 1f;

    Animator anim;
    private float invulnTimer = 0f;
    private bool invulnerable = false;

    // Start is called before the first frame update
    void Start()
    {
        if (anim == null) anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invulnerable)
            InvulnerableCD();
    }

    private void InvulnerableCD()
    {
        if (invulnTimer < invulnMaxTime)
            invulnTimer += Time.deltaTime;
        else if (invulnTimer >= invulnMaxTime)
        {
            invulnTimer = 0f;
            invulnerable = false;
        }
    }

    public void SetMovementAnim(bool status)
    {
        anim.SetBool("isWalking", status);
    }

    public void DamageTaken(float dmgVal)
    {
        if (!invulnerable)
        {
            playerManager.DamageTaken(dmgVal);
            if (hurtSFX != null)
                Camera.main.GetComponent<AudioSource>().PlayOneShot(hurtSFX);
            invulnerable = true;
        }
    }

    public void AddHealth(float value)
    {
        playerManager.AddHealth(value);
    }

}
